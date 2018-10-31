using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;

namespace Client
{
    class Program
    {
        public static void puttykeyAuth(int serverPort)
        {
            Chilkat.Ssh ssh = new Chilkat.Ssh();

            bool success = ssh.UnlockComponent("Anything for 30-day trial.");
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            //  Load a .ppk PuTTY private key.
            Chilkat.SshKey puttyKey = new Chilkat.SshKey();
            string ppkText = puttyKey.LoadText("private_key.ppk");
            //  The ppkText contains this content:

            //  	PuTTY-User-Key-File-2: ssh-rsa
            //  	Encryption: aes256-cbc
            //  	Comment: rsa-key-20170126
            //  	Public-Lines: 6
            //  	AAAAB3NzaC1yc2EAAAABJQAAAQEAx+52s7vvaZ8rT2UdFZWlSUVDHDQ+5ZRFvgRW
            //  	6nm2sO1c9WqNg7u2PQL4jeKSDX2XWcMnpleALz2x8Rr4rMy5E1iZzvWov8VtFd8l
            //  	fa9HOkgEeJB3VFuYR3NlnD3eyCoYJYPVpHJHrIeui2WZs5vQ76HDe+th8+z5Ald4
            //  	zPw3p2c6ZJpBrkSBM67hWokoBDi23c7NhszDHhJBrv+B98cQxnagI1PUKqN7E8Vg
            //  	bNtBI8beIMHyI69up9G1AXSEi0cGIjYNx9RNUPau1mRk/SvfqxgWkAjM005lj7hc
            //  	bOsjbdKK3T2NtrKTaYjEiXlEXcj1iGuApsD/m73pYaEJB3Nd7w==
            //  	Private-Lines: 14
            //  	MoaDbq0owouN/7Z9Pga0favDhM2bSEgMErJBxdDmNUXIVVcUoLiD/Ps1RA+BeBBX
            //  	wxqKUt9PqLy/pnafPR/i2xjJiQtQ0CWkPxND16Gi1dqLzmbQYYl1ev4+LzuG0zNX
            //  	HDGMvRiwagY7mY+F1tUjBYfOL6z8XHw4m40YcY1QorOO+0MMzAVT5Hkg8YyXW209
            //  	B/V/LRADFMVA2BlL39y11cb5ZpFStPH/waYUMY+2w1ZmJZ7dcRoMjuKmY+YE/tUx
            //  	n9X3P0qTNSbw6e6sMG3Dhr1vfoJUQWApUliD6GpUiCeIvXBcVqG8Vsfq9XADsPl0
            //  	+nFAwjSZflywcB7/FwhGb7q5UmcJK06SzoMl7Og5e3g7NCs3yNNQIv+qCpDjhxrA
            //  	hpT03mbipu7OXCZDeUwUhMGJAmYHE5iqm1rPCsSVbaMgpxhCWf01Cx4gLx3aMvn4
            //  	MdylA31GuL3wSxcWTslrOI8+449lZN/qZEnGEZkYTrnlu123jTqsAWMMtuHSz2Ig
            //  	6GA89oTdlppkNflhNH3OJ85kMUrc3p/ZBMdndz8jTDTljmJjHR5oNMoShFof115A
            //  	nWjUHqBwCgcubLYyH3afDvBTOhtl0tJ9Oby0wJlOAGnCXiPSDbF/y7J7xml/PS9t
            //  	XlSVNxtAY15NDO6Fp96sBVfKuJsfJ90PzdBom4ikIuf7sMwtElrHHLuYfcXJQYLp
            //  	G5jBmqDgnirosVPEBIxlxFzz/HCRmdU+tsYg46gqI4R5UpKUe8WSaJoZkDGsrqhm
            //  	e+1SJaBuafR4v2bx/bV414Hg7LGQosK2S3crxH4UgZl+g02vWznZfBH+9CmHvKDR
            //  	AxfcKOTzsaILKJtQPV81lmJ45sARYMcB5jMiE4kBg56hiXouChsvKkm55WVcW1E+
            //  	Private-MAC: 17512c9f7582c1d9c3ae2b01b4d67a6b1dbd1d0e

            //  "secret" is the actual password for the above PPK.

            puttyKey.Password = "rish_247";
            success = puttyKey.FromPuttyPrivateKey(ppkText);
            if (success != true)
            {
                Console.WriteLine(puttyKey.LastErrorText);
                return;
            }

            string sshHostname = "172.24.16.155";
            //string sshHostname = "172.24.19.59";
            int sshPort = 22;

            //  Connect to an SSH server
            success = ssh.Connect(sshHostname, sshPort);
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            //  Authenticate with the SSH server using a username + private key.
            //  (The private key serves as the password.  The username identifies
            //  the SSH user account on the server.)
            success = ssh.AuthenticatePk("harinath", puttyKey);
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            Console.WriteLine("OK, the connection and authentication with the SSH server is completed.");
        }
        public static void publickeyAuth()
        {
            Chilkat.Ssh ssh = new Chilkat.Ssh();

            //  Any string automatically begins a fully-functional 30-day trial.
            bool success = ssh.UnlockComponent("Anything for 30-day trial");
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            //  Set some timeouts, in milliseconds:
            ssh.ConnectTimeoutMs = 5000;
            ssh.IdleTimeoutMs = 15000;

            //  Connect to the SSH server.
            //  The standard SSH port = 22
            //  The hostname may be a hostname or IP address.
            int port;
            string hostname;
            hostname = "172.24.16.155";
            port = 22;
            success = ssh.Connect(hostname, port);
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            Chilkat.SshKey key = new Chilkat.SshKey();

            //  Read the PEM file into a string variable:
            //  (This does not load the PEM file into the key.  The LoadText
            //  method is a convenience method for loading the full contents of ANY text
            //  file into a string variable.)
            string privKey = key.LoadText("private.ppk");
            if (key.LastMethodSuccess != true)
            {
                Console.WriteLine(key.LastErrorText);
                return;
            }

            //  Load a private key from a PEM string:
            //  (Private keys may be loaded from OpenSSH and Putty formats.
            //  Both encrypted and unencrypted private key file formats
            //  are supported.  This example loads an unencrypted private
            //  key in OpenSSH format.  PuTTY keys typically use the .ppk
            //  file extension, while OpenSSH keys use the PEM format.
            //  (For PuTTY keys, call FromPuttyPrivateKey instead.)
            success = key.FromOpenSshPrivateKey(privKey);
            if (success != true)
            {
                Console.WriteLine(key.LastErrorText);
                return;
            }

            //  Authenticate with the SSH server using the login and
            //  private key.  (The corresponding public key should've
            //  been installed on the SSH server beforehand.)
            success = ssh.AuthenticatePk("myLogin", key);
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            Console.WriteLine(ssh.LastErrorText);
            Console.WriteLine("Public-Key Authentication Successful!");
        }

        public static void tunnel(int serverPort)
        {
            Chilkat.Socket tunnel = new Chilkat.Socket();

            bool success;

            //  Anything unlocks the component and begins a fully-functional 30-day trial.
            success = tunnel.UnlockComponent("Anything for 30-day trial");
            if (success != true)
            {
                Console.WriteLine(tunnel.LastErrorText);
                return;
            }

            string sshHostname = "172.24.19.18";
            int sshPort = 22;

            //  Connect to an SSH server and establish the SSH tunnel:
            success = tunnel.SshOpenTunnel(sshHostname, sshPort);
            if (success != true)
            {
                Console.WriteLine(tunnel.LastErrorText);
                return;
            }

            //  Authenticate with the SSH server via a login/password
            //  or with a public key.
            //  This example demonstrates SSH password authentication.
            success = tunnel.SshAuthenticatePw("rishabh", "root123");
            if (success != true)
            {
                Console.WriteLine(tunnel.LastErrorText);
                return;
            }

            //  OK, the SSH tunnel is setup.  Now open a channel within the tunnel.
            //  Once the channel is obtained, the Socket API may
            //  be used exactly the same as usual, except all communications
            //  are sent through the channel in the SSH tunnel.
            //  Any number of channels may be created from the same SSH tunnel.
            //  Multiple channels may coexist at the same time.

            //  Connect to an NIST time server and read the current date/time
            Chilkat.Socket channel = null;
            int maxWaitMs = 4000;
            bool useTls = false;
            channel = tunnel.SshOpenChannel("time-c-g.nist.gov", 37, useTls, maxWaitMs);
            if (channel == null)
            {
                Console.WriteLine(tunnel.LastErrorText);
                return;
            }

            //  The time server will send a big-endian 32-bit integer representing
            //  the number of seconds since since 00:00 (midnight) 1 January 1900 GMT.
            //  The ReceiveInt32 method will receive a 4-byte integer, but returns
            //  true or false to indicate success.  If successful, the integer
            //  is obtained via the ReceivedInt property.
            bool bigEndian = true;
            success = channel.ReceiveInt32(bigEndian);
            if (success != true)
            {
                Console.WriteLine(channel.LastErrorText);

                return;
            }

            Chilkat.CkDateTime dt = new Chilkat.CkDateTime();
            dt.SetFromNtpTime(channel.ReceivedInt);

            //  Show the current local date/time
            bool bLocalTime = true;
            Console.WriteLine("Current local date/time: " + dt.GetAsRfc822(bLocalTime));

            //  Close the SSH channel.
            success = channel.Close(maxWaitMs);
            if (success != true)
            {
                Console.WriteLine(channel.LastErrorText);

                return;
            }

            //  It is possible to create a new channel from the existing SSH tunnel for the next connection:
            //  Any number of channels may be created from the same SSH tunnel.
            //  Multiple channels may coexist at the same time.
            channel = tunnel.SshOpenChannel("time-a.nist.gov", 37, useTls, maxWaitMs);
            if (channel == null)
            {
                Console.WriteLine(tunnel.LastErrorText);
                return;
            }

            //  Review the LastErrorText to see that the connection was made via the SSH tunnel:
            Console.WriteLine(tunnel.LastErrorText);

            //  Close the connection to time-a.nist.gov.  This is actually closing our channel
            //  within the SSH tunnel, but keeps the tunnel open for the next port-forwarded connection.
            success = channel.Close(maxWaitMs);
            if (success != true)
            {
                Console.WriteLine(channel.LastErrorText);

                return;
            }

            //  Finally, close the SSH tunnel.
            success = tunnel.SshCloseTunnel();
            if (success != true)
            {
                Console.WriteLine(tunnel.LastErrorText);
                return;
            }

            Console.WriteLine("TCP SSH tunneling example completed.");
        }
        public static void newMethod()
        {
            //  This example assumes Chilkat SSH/SFTP to have been previously unlocked.
            //  See Unlock SSH for sample code.

            Chilkat.Ssh ssh = new Chilkat.Ssh();

            //  Any string automatically begins a fully-functional 30-day trial.
            bool suc = ssh.UnlockComponent("Anything for 30-day trial");
            if (suc != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }
            int port = 22;
            bool success = ssh.Connect("172.24.16.155", port);
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            //  Authenticate using login/password:
            success = ssh.AuthenticatePw("harinath", "1234");
            if (success != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            //  Send some commands and get the output.
            string strOutput = ssh.QuickCommand("df", "ansi");
            if (ssh.LastMethodSuccess != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            Console.WriteLine("---- df ----");
            Console.WriteLine(strOutput);

            strOutput = ssh.QuickCommand("echo hello world", "ansi");
            if (ssh.LastMethodSuccess != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            Console.WriteLine("---- echo hello world ----");
            Console.WriteLine(strOutput);

            strOutput = ssh.QuickCommand("date", "ansi");
            if (ssh.LastMethodSuccess != true)
            {
                Console.WriteLine(ssh.LastErrorText);
                return;
            }

            Console.WriteLine("---- date ----");
            Console.WriteLine(strOutput);
        }
        static void Main(string[] args)
        {
            //Request server IP and port number
            Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            string serverInfo = Console.ReadLine();

            //Parse the necessary information out of the provided string
            string serverIP = serverInfo.Split(':').First();
            int serverPort = int.Parse(serverInfo.Split(':').Last());

            //Keep a loopcounter
            int loopCounter = 1;
            while (true)
            {
                //Write some information to the console window
                string messageToSend = "This is message #" + loopCounter;
                Console.WriteLine("Sending message to server saying '" + messageToSend + "'");

                //Send the message in a single line
                NetworkComms.SendObject("Message", serverIP, serverPort, messageToSend);

                //Check if user wants to go around the loop
                Console.WriteLine("\nPress q to quit or any other key to send another message.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
                else loopCounter++;
            }
            //puttykeyAuth(serverPort);
            //publickeyAuth();
            //newMethod();
            tunnel(serverPort);
            Console.Read();
            //We have used comms so we make sure to call shutdown
            NetworkComms.Shutdown();
        }
    }
}