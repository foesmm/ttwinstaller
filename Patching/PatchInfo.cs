﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaleOfTwoWastelands.Patching
{
    [Serializable]
    public class PatchInfo
    {
        public FileValidation Metadata { get; set; }
        public byte[] Data { get; set; }

#if BUILD_PATCHDB
        public static PatchInfo FromFile(string prefix, string oldFilename, string newFilename)
        {
            var oldChksum = Util.GetChecksum(oldFilename);
            var newChksum = Util.GetChecksum(newFilename);
            prefix = !string.IsNullOrEmpty(prefix) ? System.IO.Path.Combine(BSADiff.PatchDir, prefix) : BSADiff.PatchDir;

            var diffPath = System.IO.Path.Combine(prefix, System.IO.Path.GetFileName(oldFilename) + "." + oldChksum + "." + newChksum + ".diff");
            return new PatchInfo()
            {
                Metadata = FileValidation.FromFile(oldFilename),
                Data = System.IO.File.Exists(diffPath) ? System.IO.File.ReadAllBytes(diffPath) : null
            };
        }
#endif
    }
}
