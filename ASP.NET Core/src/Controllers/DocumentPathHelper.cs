using System;
using System.IO;

namespace SyncfusionDocument.Controllers
{
    // Centralizes path-traversal-safe resolution of user-supplied document names so every
    // Save/Import/Load endpoint that writes into the documents folder shares one implementation.
    internal static class DocumentPathHelper
    {
        public static string ResolveSafeDocumentPath(string baseDirectory, string userSuppliedName, Func<string, bool> isSupportedExtension)
        {
            string safeName = Path.GetFileName(userSuppliedName ?? string.Empty);
            string extension = Path.GetExtension(safeName).ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(safeName) || !isSupportedExtension(extension))
                throw new ArgumentException("Invalid or unsupported document name.");

            string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, safeName));
            string fullBase = Path.GetFullPath(baseDirectory) + Path.DirectorySeparatorChar;
            if (!fullPath.StartsWith(fullBase, StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Resolved path escapes the document root.");
            return fullPath;
        }
    }
}
