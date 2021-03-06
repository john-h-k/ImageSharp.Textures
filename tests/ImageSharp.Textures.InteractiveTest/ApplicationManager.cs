﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid;

namespace Phoenix.Import.Application
{
    public static class ApplicationManager
    {
        public static CommandList CommandList { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static ImGuiRenderer Controller { get; set; }

        private static readonly object lockObject = new object();

        public static unsafe IntPtr Create(Image<Rgba32> image)
        {
            lock (lockObject)
            {
                Texture texture = GraphicsDevice.ResourceFactory.CreateTexture(TextureDescription.Texture2D((uint)image.Width, (uint)image.Height, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Sampled));
                fixed (void* pin = &MemoryMarshal.GetReference(image.GetPixelSpan()))
                {
                    GraphicsDevice.UpdateTexture(texture, (IntPtr)pin, (uint)(4 * image.Width * image.Height), 0, 0, 0, (uint)image.Width, (uint)image.Height, 1, 0, 0);
                }
                return Controller.GetOrCreateImGuiBinding(GraphicsDevice.ResourceFactory, texture);
            }
        }

        public static void ClearImageCache()
        {
            Controller.ClearCachedImageResources();
        }

        private static Dictionary<string, object> _datastore;
        public static Dictionary<string, object> DataStore
        {
            get
            {
                if (_datastore == null)
                {
                    _datastore = new Dictionary<string, object>();
                }
                return _datastore;
            }
        }
    }
}
