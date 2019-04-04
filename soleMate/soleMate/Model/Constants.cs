using System;
using System.Collections.Generic;

namespace soleMate.Model {
    public static class Constants {

        public static class LoginButton {
            public static int loginAttempts = 0; 
        }

        public static class Button {
            public const int widthLong = 186;
            public const int widthShort = 136;
            public const int height = 42;
            public const string mainBackgroundColour = "#D33F49";
            public const string secondaryBackgroundColour = "#0C7C59";
            public const string thirdBackgroundColour = "#FED766";
            public const string disabled = "#50514F";
            public const int imageWidth = 37;
            public const int imageHeight = 37;
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
            public const int highPriceRange = 0;
            public const bool sortLowToHigh = true;
            public const string sortLowestText = "Lowest";
            public const string sortHighestText = "Highest";
        }

        public static class SearchItem {
            public const string overlayBackgroundColour = "#D33F49";
            public const int imageWidth = 140;
            public const int imageHeight = 140;
            public const int outlineWidth = 160;
            public const int outlineHeight = 165;
            public const int overlayHeight = 25;
            public const string outlineColour = "#D33F49";
            public const double opacity = 0.50;
            public const int numberOfTapsRequired = 1;
        }

        public static class WatchListItem {
            public const string overlayBackgroundColour = "#D33F49";
            public const string outlineColour = "#D33F49";
            public const int outlineWidth = 160;
            public const int outlineHeight = 100;
        }

        public static class Text {
            public const string green = "#0C7C59";
            public const string red = "#D33F49";
        }

        public static class EmptyState {
            public const string searchResultTextMain = "Sorry! No results were found";
            public const string searchResultTextSecondary = "Please try another search";
            public const string watchListTextMain = "Your Watchlist is Empty";
        }
    }
}
