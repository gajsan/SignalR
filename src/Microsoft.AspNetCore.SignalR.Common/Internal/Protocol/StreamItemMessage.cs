// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Internal;

namespace Microsoft.AspNetCore.SignalR.Internal.Protocol
{
    public class StreamItemMessage : HubMessage, IEquatable<StreamItemMessage>
    {
        public object Item { get; }

        public StreamItemMessage(string invocationId, object item) : base(invocationId)
        {
            Item = item;
        }

        public override bool Equals(object obj)
        {
            return obj is StreamItemMessage m && Equals(m);
        }

        public override int GetHashCode()
        {
            var combiner = new HashCodeCombiner();
            combiner.Add(InvocationId);
            combiner.Add(Item);
            return combiner.CombinedHash;
        }

        public bool Equals(StreamItemMessage other)
        {
            return string.Equals(InvocationId, other.InvocationId, StringComparison.Ordinal) &&
                Equals(Item, other.Item);
        }

        public override string ToString()
        {
            return $"StreamItem {{ {nameof(InvocationId)}: \"{InvocationId}\", {nameof(Item)}: {Item ?? "<<null>>"} }}";
        }
    }
}
