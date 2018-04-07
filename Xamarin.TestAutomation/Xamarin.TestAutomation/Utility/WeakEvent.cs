using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mobile.Utility
{
	public abstract class WeakEvent
	{
		private readonly HashSet<Handler> _handlerSet = new HashSet<Handler>();
		private Handler[] _handlerArr = null;

		protected int InternalHandlerCount { get { return _handlerSet.Count; } }

		private IEnumerable<Handler> Handlers
		{
			get
			{
				var handlerArr = _handlerArr;
				if(handlerArr == null)
				{
					lock(_handlerSet)
					{
						handlerArr = _handlerArr = _handlerSet.ToArray();
					}
				}
				foreach(var handler in handlerArr)
				{
					if(handler.IsAlive == false)
					{
						RemoveHandler(handler);
					}
					else
					{
						yield return handler;
					}
				}
			}
		}

		protected void InternalRaiseEvent(params object[] args)
		{
			foreach(var d in Handlers)
			{
				d.Invoke(args);
			}
		}

		protected void InternalAddHandler(Delegate handlerDelegate)
		{
			var handler = Handler.Create(handlerDelegate);
			AddHandler(handler);
		}

		protected void InternalRemoveHandler(Delegate handlerDelegate)
		{
			foreach(var handler in Handlers)
			{
				if(handler.IsDelegateOf(handlerDelegate))
				{
					RemoveHandler(handler);
					return;
				}
			}
		}

		protected void InternalClearAllHandlers()
		{
			lock(_handlerSet)
			{
				_handlerSet.Clear();
				_handlerArr = null;
			}
		}

		private void AddHandler(Handler handler)
		{
			lock(_handlerSet)
			{
				_handlerSet.Add(handler);
				_handlerArr = null;
			}
		}

		private void RemoveHandler(Handler handler)
		{
			lock(_handlerSet)
			{
				_handlerSet.Remove(handler);
				_handlerArr = null;
			}
		}

		private abstract class Handler
		{
			private MethodInfo _method;

			private Handler(Delegate handlerDelegate)
			{
				_method = handlerDelegate.GetMethodInfo();
			}

			public abstract bool IsDelegateOf(Delegate handlerDelegate);
			public abstract void Invoke(object[] args);
			public abstract bool IsAlive { get; }

			public static Handler Create(Delegate handlerDelegate)
			{
				if(handlerDelegate.Target != null)
				{
					return new WeakHandler(handlerDelegate);
				}
				return new StaticHandler(handlerDelegate);
			}

			private class WeakHandler : Handler
			{
				private WeakReference _weakRef;

				public override bool IsAlive { get { return _weakRef.IsAlive; } }

				public WeakHandler(Delegate handlerDelegate) : base(handlerDelegate)
				{
					_weakRef = new WeakReference(handlerDelegate.Target);
				}

				public override bool IsDelegateOf(Delegate handlerDelegate)
				{
					return (handlerDelegate.Target == _weakRef.Target && handlerDelegate.GetMethodInfo().Equals(_method));
				}

				public override void Invoke(object[] args)
				{
					object target = _weakRef.Target;
					if(target != null)
					{
						_method.Invoke(target, args);
					}
				}
			}

			private class StaticHandler : Handler
			{
				public override bool IsAlive { get { return true; } }

				public StaticHandler(Delegate handlerDelegate) : base(handlerDelegate)
				{
				}

				public override bool IsDelegateOf(Delegate handlerDelegate)
				{
					return (handlerDelegate.Target == null && handlerDelegate.GetMethodInfo().Equals(_method));
				}

				public override void Invoke(object[] args)
				{
					_method.Invoke(null, args);
				}
			}
		}
	}

	public class WeakEvent<TEventArgs> : WeakEvent
	{
		public void Add(EventHandler<TEventArgs> handler)
		{
			InternalAddHandler(handler);
		}

		public void Remove(EventHandler<TEventArgs> handler)
		{
			InternalRemoveHandler(handler);
		}

		public class Source : WeakEvent<TEventArgs>
		{
			public int HandlerCount { get { return InternalHandlerCount; } }

			public void RaiseEvent(object sender, TEventArgs args)
			{
				InternalRaiseEvent(sender, args);
			}
		}
	}
}
