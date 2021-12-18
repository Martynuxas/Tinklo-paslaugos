using System;
using System.Runtime.InteropServices;

namespace Common.Util 
{
	/// <summary>
	/// Helpers related to marshaling.
	/// </summary>
	public class MarshalUtil 
	{
		/// <summary>
		/// Convert given byte array to a copy of structure of given type.
		/// </summary>
		/// <typeparam name="T">Type of structure to extract.</typeparam>
		/// <param name="src">Byte array to use.</param>
		/// <returns>Structure of given type extracted from given byte array.</returns>
		public static T BufferToStruct<T>(byte[] src) where T : struct
		{
			//validate inputs
			if( src == null ) throw new ArgumentException("Argument 'src' is null.");
			if( src.Length < Marshal.SizeOf<T>() ) throw new ArgumentException($"Argument 'src' is too short. At least '{Marshal.SizeOf<T>()} bytes needed'.");

			//allocate marshaling buffer
			var marshalBufPtr = Marshal.AllocHGlobal(Marshal.SizeOf<T>());

			//
			try {
				//copy given data to marshaling buffer and get the request type out of it
				Marshal.Copy(src, 0, marshalBufPtr, Marshal.SizeOf<T>());
				var dst = (T)Marshal.PtrToStructure(marshalBufPtr, typeof(T));

				//
				return dst;
			}
			finally
			{
				//release resources
				Marshal.FreeHGlobal(marshalBufPtr);
				marshalBufPtr = IntPtr.Zero;
			}			
		}

		/// <summary>
		/// Convert given structure to a copy of byte array.
		/// </summary>
		/// <typeparam name="T">Type of structure, to enforce value types only.static</typeparam>
		/// <param name="src">Structure to convert.</param>
		/// <returns>A byte array corresponding to given structure.</return>
		public static byte[] StructToBuffer<T>(T src) where T : struct
		{
			var dst = new byte[Marshal.SizeOf<T>()];

			//allocate marshaling buffer
			var marshalBufPtr = Marshal.AllocHGlobal(Marshal.SizeOf<T>());

			//
			try {
				//marshal given instance to marshalling buffer and get the byte array out of it
				Marshal.StructureToPtr(src, marshalBufPtr, false);
				Marshal.Copy(marshalBufPtr, dst, 0, Marshal.SizeOf<T>());

				//
				return dst;
			}
			finally
			{
				//release resources
				Marshal.FreeHGlobal(marshalBufPtr);
				marshalBufPtr = IntPtr.Zero;
			}
		}
	}
}