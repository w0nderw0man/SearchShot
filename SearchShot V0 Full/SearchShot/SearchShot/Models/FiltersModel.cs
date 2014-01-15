using System;
using System.Collections.Generic;

namespace SearchShot.Models
{
    public class FiltersModel
    {
        #region Properties

        public List<FilterModel> ArtisticFilters { get; private set; }
        public List<FilterModel> EnhancementFilters { get; private set; }
        public List<FilterModel> MoveFilters { get; private set; }
        public List<FilterModel> ColorFilters { get; private set; }

        #endregion

        public FiltersModel()
        {
            LoadArtisticFilters();
            LoadEnhancementFilters();
            LoadColorFilters();
            LoadMoveFilters();
        }

        public FilterModel RandomFilter()
        {
            int seed = unchecked((int)DateTime.Now.Ticks);

            Random random = new Random(seed);

            int index = random.Next(ArtisticFilters.Count + EnhancementFilters.Count - 1);

            return index < ArtisticFilters.Count ? ArtisticFilters[index] : EnhancementFilters[index - ArtisticFilters.Count];
        }

        #region Private methods

        private void LoadArtisticFilters()
        {
            ArtisticFilters = new List<FilterModel>();

            ArtisticFilters.Add(new AntiqueFilterModel());
            /// todo Blend
            //ArtisticFilters.Add(new BlurFilterModel()); // taille
            ArtisticFilters.Add(new BrightnessFilterModel());
            //ArtisticFilters.Add(new CartoonFilterModel()); //taille
            /// todo Colorization
            /// todo ColorSwap
            ArtisticFilters.Add(new ContrastFilterModel());
            /// todo Curves
            //ArtisticFilters.Add(new DespeckleFilterModel()); //taille
            //ArtisticFilters.Add(new EmbossFilterModel()); // moche
            /// todo Frame
            /// todo FreeRotation
            ArtisticFilters.Add(new GrayscaleFilterModel());
            /// todo GrayscaleNegative !
            ArtisticFilters.Add(new HueSaturationFilterModel());
            /// todo ImageFusion
            ArtisticFilters.Add(new LomoFilterModel());
            //ArtisticFilters.Add(new MagicPenFilterModel()); // moche
            ArtisticFilters.Add(new MilkyFilterModel());
            ArtisticFilters.Add(new MirrorFilterModel());
            ArtisticFilters.Add(new MoonlightFilterModel());
            ArtisticFilters.Add(new NegativeFilterModel());
            //ArtisticFilters.Add(new OilyFilterModel()); //lent
            ArtisticFilters.Add(new PaintFilterModel());
            ArtisticFilters.Add(new PosterizeFilterModel());
            ArtisticFilters.Add(new SepiaFilterModel());
            ArtisticFilters.Add(new SharpnessFilterModel());
            ArtisticFilters.Add(new SketchFilterModel());
            //ArtisticFilters.Add(new SolarizeFilterModel()); // moche
            /// todo SplitTone
            /// todo Spotlight
            //ArtisticFilters.Add(new StampFilterModel()); // moche
            ArtisticFilters.Add(new WarpTwisterFilterModel());
            ArtisticFilters.Add(new WatercolorFilterModel());
            ArtisticFilters.Add(new VignettingFilterModel());
        }

        private void LoadEnhancementFilters()
        {
            EnhancementFilters = new List<FilterModel>();

            EnhancementFilters.Add(new AutoEnhanceFilterModel());
            EnhancementFilters.Add(new AutoLevelsFilterModel());
            EnhancementFilters.Add(new ColorBoostFilterModel());
            EnhancementFilters.Add(new ExposureFilterModel());
            EnhancementFilters.Add(new FoundationFilterModel());
            EnhancementFilters.Add(new LevelsFilterModel());
            EnhancementFilters.Add(new LocalBoostFilterModel());
            EnhancementFilters.Add(new TemperatureAndTintFilterModel());
            /// todo WhiteBalance
            EnhancementFilters.Add(new WhiteboardEnhancementFilterModel());
        }
        private void LoadMoveFilters()
        {
            MoveFilters = new List<FilterModel>();

            MoveFilters.Add(new FlipFilterModel());
            MoveFilters.Add(new StepRotationLeftFilterModel());
            MoveFilters.Add(new StepRotationRightFilterModel());
        }
        private void LoadColorFilters()
        {
            ColorFilters = new List<FilterModel>();

            ColorFilters.Add(new MonocolorRedFilterModel());
            ColorFilters.Add(new MonocolorGreenFilterModel());
            ColorFilters.Add(new MonocolorBlueFilterModel());
            ColorFilters.Add(new ColorAdjustRedFilterModel());
            ColorFilters.Add(new ColorAdjustGreenFilterModel());
            ColorFilters.Add(new ColorAdjustBlueFilterModel());
        }

        #endregion
    }
}
