using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public static class UnmanagedMemory
{
	/// <summary>
	/// Keep track of all the allocations that haven't been freed
	/// </summary>
	private static readonly HashSet<IntPtr> allocations = new HashSet<IntPtr>();
 
	/// <summary>
	/// Allocate unmanaged heap memory and track it
	/// </summary>
	/// <param name="size">Number of bytes of unmanaged heap memory to allocate</param>
	public static IntPtr Alloc(int size)
	{
		var ptr = Marshal.AllocHGlobal(size);
		#if UNITY_EDITOR
			allocations.Add(ptr);
		#endif
		return ptr;
	}
 
	/// <summary>
	/// Free unmanaged heap memory and stop tracking it
	/// </summary>
	/// <param name="ptr">Pointer to the unmanaged heap memory to free</param>
	public static void Free(IntPtr ptr)
	{
		Marshal.FreeHGlobal(ptr);
		allocations.Remove(ptr);
	}
 
	/// <summary>
	/// Free all unmanaged heap memory allocated with <see cref="Alloc"/>
	/// </summary>
	public static void Cleanup()
	{
		foreach (var ptr in allocations)
		{
			Marshal.FreeHGlobal(ptr);
		}
		allocations.Clear();
	}
}