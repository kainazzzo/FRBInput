/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Specialized;
using Apache.NMS.Util;

namespace Apache.NMS.Stomp.Transport.Failover
{
	public class FailoverTransportFactory : ITransportFactory
	{
		private ITransport doConnect(Uri location)
		{
			ITransport transport = CreateTransport(URISupport.ParseComposite(location));
			transport = new MutexTransport(transport);
			transport = new ResponseCorrelator(transport);
			return transport;
		}

		public ITransport CompositeConnect(Uri location)
		{
			return CreateTransport(URISupport.ParseComposite(location));
		}

		public ITransport CompositeConnect(Uri location, SetTransport setTransport)
		{
			throw new NMSConnectionException("Asynchronous composite connection not supported with Failover transport.");
		}

		public ITransport CreateTransport(Uri location)
		{
			return doConnect(location);
		}

		/// <summary>
		/// </summary>
		/// <param name="compositData"></param>
		/// <returns></returns>
		public ITransport CreateTransport(URISupport.CompositeData compositData)
		{
			StringDictionary options = compositData.Parameters;
			FailoverTransport transport = CreateTransport(options);
			transport.Add(compositData.Components);
			return transport;
		}

		public FailoverTransport CreateTransport(StringDictionary parameters)
		{
			FailoverTransport transport = new FailoverTransport();
			URISupport.SetProperties(transport, parameters, "");
			return transport;
		}
	}
}
