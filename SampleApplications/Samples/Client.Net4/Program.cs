/* ========================================================================
 * Copyright (c) 2005-2013 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

using Opc.Ua.Server;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using Opc.Ua.Client.Controls;
using Opc.Ua.Sample.Controls;
using Opc.Ua.Bindings;
using System.Net;
using System.Net.Security;

using System.Threading;

namespace Opc.Ua.Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationName   = "UA Sample Client";
            application.ApplicationType   = ApplicationType.ClientAndServer;
            application.ConfigSectionName = "Opc.Ua.SampleClient";

            try
            {
                // load the application configuration.
                application.LoadApplicationConfiguration(false);

                // check the application certificate.
                application.CheckApplicationInstanceCertificate(false, 0);

                // start the server.
                application.Start(new SampleServer());

                // run the application interactively.
                Application.Run(new SampleClientForm(application, null, application.ApplicationConfiguration));
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, application.ApplicationName);
                return;
            }
        }
    }
}
