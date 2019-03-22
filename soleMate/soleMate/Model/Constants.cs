using System;
using System.Collections.Generic;

namespace soleMate.Model {
    public static class Constants {

        public static class Button {
            public const int widthLong = 186;
            public const int widthShort = 136;
            public const int height = 42;
            public const string mainBackgroundColour = "#D33F49";
            public const string secondaryBackgroundColour = "#0C7C59";
        }

        public static class InputField {
            public const int width = 300;
            public const int height = 42;
            public const string backgroundColour = "#E5E5E5";
        }

        public static class Slider {
            public const string minTrackColour = "#D33F49";
            public const string maxTrackColour = "#E5E5E5";
        }

        public static class SearchDefaults {
            public const int lowPriceRange = 0;
            public const int highPriceRange = 100;
            public const string sortPrice = "Lowest";
        }

        public static class SearchItem {
            public const string overlayBackgroundColour = "#E5E5E5";
            public const int imageWidth = 140;
            public const int imageHeight = 140;
            public const int outlineWidth = 160;
            public const int outlineHeight = 165;
            public const int overlayHeight = 25;
            public const double opacity = 0.5;
            public const string outlineColour = "#0C7C59";
            public const int numberOfTapsRequired = 1;
        }

        public static class Text {
            public const string green = "#0C7C59";
        }
    }
}
