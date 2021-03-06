// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Describes a Service Fabric property value of type Int64.
    /// </summary>
    public partial class Int64PropertyValue : PropertyValue
    {
        /// <summary>
        /// Initializes a new instance of the Int64PropertyValue class.
        /// </summary>
        /// <param name="data">The data of the property value.</param>
        public Int64PropertyValue(
            string data)
            : base(
                Common.PropertyValueKind.Int64)
        {
            data.ThrowIfNull(nameof(data));
            this.Data = data;
        }

        /// <summary>
        /// Gets the data of the property value.
        /// </summary>
        public string Data { get; }
    }
}
