/*  
	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at
	
	http://www.apache.org/licenses/LICENSE-2.0
	
	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using Microsoft.Phone;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using WPCordovaClassLib.Cordova.UI;
using AudioResult = WPCordovaClassLib.Cordova.UI.AudioCaptureTask.AudioResult;
using VideoResult = WPCordovaClassLib.Cordova.UI.VideoCaptureTask.VideoResult;
using System.Windows;
using System.Diagnostics;
using Microsoft.Phone.Controls;

namespace WPCordovaClassLib.Cordova.Commands
{
    /// <summary>
    /// Provides access to the audio, image, and video capture capabilities of the device
    /// </summary>
    public class Capture : BaseCommand
    {
        #region Internal classes (options and resultant objects)

        /// <summary>
        /// Represents captureImage action options.
        /// </summary>
        [DataContract]
        public class CaptureImageOptions
        {
            /// <summary>
            /// The maximum number of images the device user can capture in a single capture operation. The value must be greater than or equal to 1 (defaults to 1).
            /// </summary>
            [DataMember(IsRequired = false, Name = "limit")]
            public int Limit { get; set; }

            public static CaptureImageOptions Default
            {
                get { return new CaptureImageOptions() { Limit = 1 }; }
            }
        }

        /// <summary>
        /// Represents captureAudio action options.
        /// </summary>
        [DataContract]
        public class CaptureAudioOptions
        {
            /// <summary>
            /// The maximum number of audio files the device user can capture in a single capture operation. The value must be greater than or equal to 1 (defaults to 1).
            /// </summary>
            [DataMember(IsRequired = false, Name = "limit")]
            public int Limit { get; set; }

            public static CaptureAudioOptions Default
            {
                get { return new CaptureAudioOptions() { Limit = 1 }; }
            }
        }

        /// <summary>
        /// Represents captureVideo action options.
        /// </summary>
        [DataContract]
        public class CaptureVideoOptions
        {
            /// <summary>
            /// The maximum number of video files the device user can capture in a single capture operation. The value must be greater than or equal to 1 (defaults to 1).
            /// </summary>
            [DataMember(IsRequired = false, Name = "limit")]
            public int Limit { get; set; }

            public static CaptureVideoOptions Default
            {
                get { return new CaptureVideoOptions() { Limit = 1 }; }
            }
        }

        /// <summary>
        /// Represents getFormatData action options.
        /// </summary>
        [DataContract]
        public class MediaFormatOptions
        {
            /// <summary>
            /// File path
         