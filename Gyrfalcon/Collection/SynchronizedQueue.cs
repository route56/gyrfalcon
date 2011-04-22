using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class SynchronizedQueue<T>
	{
		#region Private members

		private Queue<T> _q;

		private object root;

		#endregion

		#region Public members

		public SynchronizedQueue(Queue<T> q)
		{
			this._q = q;
			this.root = ((ICollection)this._q).SyncRoot;
		}

		public SynchronizedQueue()
		{
			this._q = new Queue<T>();
			this.root = ((ICollection)this._q).SyncRoot;
		}

		public void Clear()
		{
			lock (this.root)
			{
				this._q.Clear();
			}
		}

		public bool Contains(T obj)
		{
			lock (this.root)
			{
				return this._q.Contains(obj);
			}
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			lock (this.root)
			{
				this._q.CopyTo(array, arrayIndex);
			}
		}

		public T Dequeue()
		{
			lock (this.root)
			{
				return this._q.Dequeue();
			}
		}

		public void Enqueue(T value)
		{
			lock (this.root)
			{
				this._q.Enqueue(value);
			}
		}

		public IEnumerator GetEnumerator()
		{
			lock (this.root)
			{
				return this._q.GetEnumerator();
			}
		}

		public object Peek()
		{
			lock (this.root)
			{
				return this._q.Peek();
			}
		}

		public T[] ToArray(bool clearQueue)
		{
			lock (this.root)
			{
				T[] array = this._q.ToArray();
				if (clearQueue)
				{
					this._q.Clear();
				}
				return array;
			}
		}

		public T[] ToArray()
		{
			lock (this.root)
			{
				return this._q.ToArray();
			}
		}

		public void TrimExcess()
		{
			lock (this.root)
			{
				this._q.TrimExcess();
			}
		}

		public int Count
		{
			get
			{
				lock (this.root)
				{
					return this._q.Count;
				}
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		public object SyncRoot
		{
			get
			{
				return this.root;
			}
		}

		#endregion
	}
}