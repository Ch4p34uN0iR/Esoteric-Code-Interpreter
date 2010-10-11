/*
 * This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://sam.zoy.org/wtfpl/COPYING for more details.
 */

using System.Collections.Generic;

namespace RoliSoft
{
    public class StackList<T> : List<T>
    {
        /// <summary>
        /// Adds an element to the top of the stack list.
        /// </summary>
        public void Push(T item)
        {
            Add(item);
        }

        /// <summary>
        /// Removes the element at the top of the stack list and returns it.
        /// </summary>
        /// <returns>The element at the top of the stack.</returns>
        public T Pop()
        {
            var pop = this[Count - 1];
            RemoveAt(Count - 1);
            return pop;
        }

        /// <summary>
        /// Specially modified <c>Pop()</c> for Befunge-93 to return zero if stack is empty.
        /// </summary>
        /// <returns>The element at the top of the stack or zero.</returns>
        public T PopZero()
        {
            return Count == 0 ? default(T) : Pop();
        }

        /// <summary>
        /// Removes the element at the top of the stack list without removing it.
        /// </summary>
        /// <returns>The element at the top of the stack.</returns>
        public T Peek()
        {
            return this[Count - 1];
        }

        /// <summary>
        /// Specially modified <c>Peek()</c> for Befunge-93 to return zero if stack is empty.
        /// </summary>
        /// <returns>The element at the top of the stack or zero.</returns>
        public T PeekZero()
        {
            return Count == 0 ? default(T) : this[Count - 1];
        }
    }
}