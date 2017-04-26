// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.Extensions.Internal;

namespace Microsoft.AspNetCore.SignalR.Internal.Protocol
{
    public class InvocationMessage : HubMessage, IEquatable<InvocationMessage>
    {
        public string Target { get; }

        public object[] Arguments { get; }

        public bool NonBlocking { get; }

        public InvocationMessage(string invocationId, bool nonBlocking, string target, params object[] arguments) : base(invocationId)
        {
            if(string.IsNullOrEmpty(invocationId))
            {
                throw new ArgumentNullException(nameof(invocationId));
            }

            if(string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if(arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            Target = target;
            Arguments = arguments;
            NonBlocking = nonBlocking;
        }

        public override bool Equals(object obj)
        {
            return obj is InvocationMessage m && Equals(m);
        }

        public override int GetHashCode()
        {
            var combiner = new HashCodeCombiner();
            combiner.Add(InvocationId);
            combiner.Add(Target);
            combiner.Add(Arguments);
            combiner.Add(NonBlocking);
            return combiner.CombinedHash;
        }

        public bool Equals(InvocationMessage other)
        {
            return string.Equals(InvocationId, other.InvocationId, StringComparison.Ordinal) &&
                string.Equals(Target, other.Target, StringComparison.Ordinal) &&
                Enumerable.SequenceEqual(Arguments, other.Arguments) &&
                NonBlocking == other.NonBlocking;
        }

        public override string ToString()
        {
            return $"Invocation {{ {nameof(InvocationId)}: \"{InvocationId}\", {nameof(NonBlocking)}: {NonBlocking}, {nameof(Target)}: \"{Target}\", {nameof(Arguments)}: [ {string.Join(", ", Arguments.Select(a => a?.ToString()))} ] }}";
        }
    }
}
