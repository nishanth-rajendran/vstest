﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if NETFRAMEWORK || NETSTANDARD2_0

using System;
using System.Reflection;

using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;

namespace Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;

/// <inheritdoc/>
public class PlatformAssemblyResolver : IAssemblyResolver
{
    /// <summary>
    /// Specifies whether the resolver is disposed or not
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformAssemblyResolver"/> class.
    /// </summary>
    public PlatformAssemblyResolver()
    {
        AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolverEvent;
    }

    ~PlatformAssemblyResolver()
    {
        Dispose(false);
    }

    /// <inheritdoc/>
    public event AssemblyResolveEventHandler? AssemblyResolve;

    public void Dispose()
    {
        Dispose(true);

        // Use SupressFinalize in case a subclass
        // of this type implements a finalizer.
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolverEvent;
            }

            _disposed = true;
        }
    }

    /// <summary>
    /// Assembly Resolve event handler for App Domain - called when CLR loader cannot resolve assembly.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="eventArgs">
    /// The event Args.
    /// </param>
    /// <returns>
    /// The <see cref="Assembly"/>.
    /// </returns>
    private Assembly? AssemblyResolverEvent(object sender, object eventArgs)
    {
        return eventArgs is not ResolveEventArgs args ? null : AssemblyResolve?.Invoke(this, new AssemblyResolveEventArgs(args.Name));
    }
}

#endif
