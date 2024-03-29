using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace tcplistener_console
{
    public class multicnnNew
    {
        // Thread signal.
        public static ManualResetEvent clientConnected = new ManualResetEvent(false);

        public static void iniciar(string puerto, string addr)
        {
            IPAddress localAddr = IPAddress.Parse(addr);
            TcpListener server = new TcpListener(localAddr, int.Parse(puerto));
            server.Start();

            server.BeginAcceptTcpClient(AcceptCallback, server);
            clientConnected.WaitOne();
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            TcpListener server = (TcpListener)ar.AsyncState;
            ClientState state = new ClientState();

            // Once the accept operation completes, this callback will
            // be called.  In it, you can create a new TcpClient in much
            // the same way you did it in the synchronous code you had:

            state.client = server.EndAcceptTcpClient(ar);

            // We’re going to start reading from the client’s stream, and
            // we need a buffer for that:

            state.buffer = new byte[4096];

            // Note that the TcpClient and the byte[] are both put into
            // this “ClientState” object.  We’re going to need an easy
            // way to get at those values in the callback for the read
            // operation.

            // Next, start a new accept operation so that we can process
            // another client connection:

            server.BeginAcceptTcpClient(AcceptCallback, server);

            // Finally, start a read operation on the client we just
            // accepted.  Note that you could do this before starting the
            // accept operation; the order isn’t really important.

            state.client.GetStream().BeginRead(state.buffer, 0, state.buffer.Length, ReadCallback, state);
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            ClientState state = (ClientState)ar.AsyncState;
            int cbRead = state.client.GetStream().EndRead(ar);

            if (cbRead == 0)
            {
                // The client has closed the connection
                return;
            }
            //else
            //{
                //Console.WriteLine(“hay datos”);
            //}

            // Your data is in state.buffer, and there are cbRead
            // bytes to process in the buffer.  This number may be
            // anywhere from 1 up to the length of the buffer.
            // The i/o completes when there is _any_ data to be read,
            // not necessarily when the buffer is full.

            // So, for example:

            string strData = Encoding.ASCII.GetString(state.buffer, 0, cbRead);
            Console.WriteLine(strData);

            // For ASCII you won’t have to worry about partial characters
            // but for pretty much any other common encoding you’ll have to
            // deal with that possibility, as there’s no guarantee that an
            // entire character will be transmitted in one piece.

            // Of course, even with ASCII, you need to watch your string
            // terminations.  You’ll have to either check the read buffer
            // directly for a null terminator, or have some other means
            // of detecting the actual end of a string.  By the time the
            // string goes through the decoding process, you’ll have lost
            // that information.

            // As with the accept operation, we need to start a new read
            // operation on this client, so that we can process the next
            // bit of data that’s sent:

            state.client.GetStream().BeginRead(state.buffer, 0, state.buffer.Length, ReadCallback, state);
        }
    }
}
