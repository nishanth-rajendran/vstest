// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Microsoft.VisualStudio.TestPlatform.Extensions.TrxLogger;

using TrxLoggerResources = Microsoft.VisualStudio.TestPlatform.Extensions.TrxLogger.Resources.TrxResource;

namespace Microsoft.TestPlatform.Extensions.TrxLogger.Utility;

/// <summary>
/// Class to be used for parameter verification.
/// </summary>
internal sealed class EqtAssert
{
    /// <summary>
    /// Do not instantiate.
    /// </summary>
    private EqtAssert()
    {
    }

    /// <summary>
    /// Verifies that the specified parameter is not null, TPDebug.Asserts and throws.
    /// </summary>
    /// <param name="expression">Expression to check</param>
    /// <param name="comment">Comment to write</param>
    public static void IsTrue(bool expression, string comment)
    {
        TPDebug.Assert(expression, comment);
        if (!expression)
        {
            throw new Exception(comment);
        }
    }

    /// <summary>
    /// Verifies that the specified parameter is not null, TPDebug.Asserts and throws.
    /// </summary>
    /// <param name="parameter">Parameter to check</param>
    /// <param name="parameterName">String - parameter name</param>
    public static void ParameterNotNull([ValidatedNotNull] object? parameter, [ValidatedNotNull] string parameterName)
    {
        AssertParameterNameNotNullOrEmpty(parameterName);
        TPDebug.Assert(parameter != null, string.Format(CultureInfo.InvariantCulture, "'{0}' is null", parameterName));
        if (parameter == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    /// <summary>
    /// Verifies that the specified string parameter is neither null nor empty, TPDebug.Asserts and throws.
    /// </summary>
    /// <param name="parameter">Parameter to check</param>
    /// <param name="parameterName">String - parameter name</param>
    public static void StringNotNullOrEmpty([ValidatedNotNull] string? parameter, [ValidatedNotNull] string parameterName)
    {
        AssertParameterNameNotNullOrEmpty(parameterName);
        TPDebug.Assert(!parameter.IsNullOrEmpty(), string.Format(CultureInfo.InvariantCulture, "'{0}' is null or empty", parameterName));
        if (parameter.IsNullOrEmpty())
        {
            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, TrxLoggerResources.Common_CannotBeNullOrEmpty));
        }
    }

    /// <summary>
    /// Asserts that the parameter name is not null or empty
    /// </summary>
    /// <param name="parameterName">The parameter name to verify</param>
    [Conditional("DEBUG")]
    private static void AssertParameterNameNotNullOrEmpty([ValidatedNotNull] string? parameterName)
    {
        TPDebug.Assert(!parameterName.IsNullOrEmpty(), "'parameterName' is null or empty");
    }
}
