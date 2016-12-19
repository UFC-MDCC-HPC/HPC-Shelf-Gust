/* Copyright (C) 2007  The Trustees of Indiana University
 *
 * Use, modification and distribution is subject to the Boost Software
 * License, Version 1.0. (See accompanying file LICENSE_1_0.txt or copy at
 * http://www.boost.org/LICENSE_1_0.txt)
 *  
 * Authors: Douglas Gregor
 *          Andrew Lumsdaine
 */
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace br.ufc.mdcc.hpc.storm.binding.channel.Binding
{
    // MPI data type definitions
#if MPI_HANDLES_ARE_POINTERS
    using MPI_Aint = IntPtr;
    using MPI_Comm = IntPtr;
    using MPI_Datatype = IntPtr;
    using MPI_Errhandler = IntPtr;
    using MPI_File = IntPtr;
    using MPI_Group = IntPtr;
    using MPI_Info = IntPtr;
    using MPI_Op = IntPtr;
    using MPI_Request = IntPtr;
    using MPI_User_function = IntPtr;
    using MPI_Win = IntPtr;
#else
    using MPI_Aint = IntPtr;
    using MPI_Comm = Int32;
    using MPI_Datatype = Int32;
    using MPI_Errhandler = Int32;
    using MPI_File = IntPtr;
    using MPI_Group = Int32;
    using MPI_Info = Int32;
    using MPI_Op = Int32;
    using MPI_Request = Int32;
    using MPI_User_function = IntPtr;
    using MPI_Win = Int32;
#endif

    /// <summary>
    /// A non-blocking communication request.
    /// </summary>
    /// <remarks>
    /// Each request object refers to a single
    /// communication operation, such as non-blocking send 
    /// (see <see cref="Communicator.ImmediateSend&lt;T&gt;(T, int, int)"/>)
    /// or receive. Non-blocking operations may progress in the background, and can complete
    /// without any user intervention. However, it is crucial that outstanding communication
    /// requests be completed with a successful call to <see cref="Wait"/> or <see cref="Test"/>
    /// before the request object is lost.
    /// </remarks>
    public abstract class Request
    {
        /// <summary>
        /// Wait until this non-blocking operation has completed.
        /// </summary>
        /// <returns>
        ///   Information about the completed communication operation.
        /// </returns>
        public abstract CompletedStatus Wait();

        /// <summary>
        /// Determine whether this non-blocking operation has completed.
        /// </summary>
        /// <returns>
        /// If the non-blocking operation has completed, returns information
        /// about the completed communication operation. Otherwise, returns
        /// <c>null</c> to indicate that the operation has not completed.
        /// </returns>
        public abstract CompletedStatus Test();

        /// <summary>
        /// Cancel this communication request.
        /// </summary>
        public abstract void Cancel();
    }

    /// <summary>
    /// A non-blocking receive request. 
    /// </summary>
    /// <remarks>
    /// This class allows one to test a receive
    /// request for completion, wait for completion of a request, cancel a request,
    /// or extract the value received by this communication request.
    /// </remarks>
    public abstract class ReceiveRequest : Request
    {
        /// <summary>
        /// Retrieve the value received via this communication. The value
        /// will only be available when the communication has completed.
        /// </summary>
        /// <returns>The value received by this communication.</returns>
        public abstract object GetValue();
    };




}
