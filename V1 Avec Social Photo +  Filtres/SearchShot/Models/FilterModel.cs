using System.Collections.Generic;
using System.Xml.Serialization;
using Nokia.Graphics.Imaging;

namespace SearchShot.Models
{
    public abstract class FilterModel
    {
        #region Properties

        [XmlIgnore]
        public string Name { get; set; }

        [XmlIgnore]
        abstract public Queue<IFilter> Components { get; }

        #endregion
    }

    #region Art filters

    public class AntiqueFilterModel : FilterModel
    {
        public AntiqueFilterModel()
        {
            Name = "Antique";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();
                components.Enqueue(new AntiqueFilter());
                return components;
            }
        }
    }

    public class BlurFilterModel : FilterModel
    {
        public BlurFilterModel()
        {
            Name = "Estomper";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new BlurFilter(5));
                return components;
            }
        }
    }

    public class BrightnessFilterModel : FilterModel
    {
        public BrightnessFilterModel()
        {
            Name = "Luminosité";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new BrightnessFilter(0.35f));

                return components;
            }
        }
    }

    public class CartoonFilterModel : FilterModel
    {
        public CartoonFilterModel()
        {
            Name = "Cartoon";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new CartoonFilter(true));

                return components;
            }
        }
    }

    public class ColorAdjustRedFilterModel : FilterModel
    {
        public ColorAdjustRedFilterModel()
        {
            Name = "Ajustement Rouge";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new ColorAdjustFilter(1.0f, 0.0f, 0.0f));

                return components;
            }
        }
    }

    public class ColorAdjustGreenFilterModel : FilterModel
    {
        public ColorAdjustGreenFilterModel()
        {
            Name = "Ajustement Vert";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new ColorAdjustFilter(0.0f, 1.0f, 0.0f));

                return components;
            }
        }
    }

    public class ColorAdjustBlueFilterModel : FilterModel
    {
        public ColorAdjustBlueFilterModel()
        {
            Name = "Ajustement Bleu";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new ColorAdjustFilter(0.0f, 0.0f, 1.0f));

                return components;
            }
        }
    }

    public class ContrastFilterModel : FilterModel
    {
        public ContrastFilterModel()
        {
            Name = "Contraste";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new ContrastFilter(0.8f));

                return components;
            }
        }
    }

    public class DespeckleFilterModel : FilterModel
    {
        public DespeckleFilterModel()
        {
            Name = "Dépoussiérer";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new DespeckleFilter(DespeckleLevel.High));

                return components;
            }
        }
    }

    public class EmbossFilterModel : FilterModel
    {
        public EmbossFilterModel()
        {
            Name = "Bosseler";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new EmbossFilter(1.0f));

                return components;
            }
        }
    }

    public class FlipFilterModel : FilterModel
    {
        public FlipFilterModel()
        {
            Name = "Retourner";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new FlipFilter(FlipMode.Horizontal));

                return components;
            }
        }
    }

    public class GrayscaleFilterModel : FilterModel
    {
        public GrayscaleFilterModel()
        {
            Name = "Niveaux de gris";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new GrayscaleFilter());

                return components;
            }
        }
    }

    public class HueSaturationFilterModel : FilterModel
    {
        public HueSaturationFilterModel()
        {
            Name = "Saturation";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new HueSaturationFilter(0.9, 0.9));

                return components;
            }
        }
    }

    public class LomoFilterModel : FilterModel
    {
        public LomoFilterModel()
        {
            Name = "Lomo";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new LomoFilter(0.5f, 0.75f, LomoVignetting.Medium, LomoStyle.Neutral));

                return components;
            }
        }
    }

    public class MagicPenFilterModel : FilterModel
    {
        public MagicPenFilterModel()
        {
            Name = "Stylo Magique";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new MagicPenFilter());

                return components;
            }
        }
    }

    public class MilkyFilterModel : FilterModel
    {
        public MilkyFilterModel()
        {
            Name = "Milky";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new MilkyFilter());

                return components;
            }
        }
    }

    public class MirrorFilterModel : FilterModel
    {
        public MirrorFilterModel()
        {
            Name = "Miroir";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new MirrorFilter());

                return components;
            }
        }
    }

    public class MonocolorRedFilterModel : FilterModel
    {
        public MonocolorRedFilterModel()
        {
            Name = "Rouge Monochrome";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new MonoColorFilter(new Windows.UI.Color() { R = 0xff, G = 0x00, B = 0x00 }, 0.3));

                return components;
            }
        }
    }

    public class MonocolorGreenFilterModel : FilterModel
    {
        public MonocolorGreenFilterModel()
        {
            Name = "Vert Monochrome";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new MonoColorFilter(new Windows.UI.Color() { R = 0x00, G = 0xff, B = 0x00 }, 0.3));

                return components;
            }
        }
    }

    public class MonocolorBlueFilterModel : FilterModel
    {
        public MonocolorBlueFilterModel()
        {
            Name = "Bleu Monochrome";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new MonoColorFilter(new Windows.UI.Color { R = 0x00, G = 0x00, B = 0xff }, 0.3));

                return components;
            }
        }
    }


    public class MoonlightFilterModel : FilterModel
    {
        public MoonlightFilterModel()
        {
            Name = "Moonlight";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new MoonlightFilter(21));

                return components;
            }
        }
    }

    public class NegativeFilterModel : FilterModel
    {
        public NegativeFilterModel()
        {
            Name = "Negatif";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new NegativeFilter());

                return components;
            }
        }
    }

    public class OilyFilterModel : FilterModel
    {
        public OilyFilterModel()
        {
            Name = "Huilé";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new OilyFilter());

                return components;
            }
        }
    }

    public class PaintFilterModel : FilterModel
    {
        public PaintFilterModel()
        {
            Name = "Peint";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new PaintFilter(1));

                return components;
            }
        }
    }

    public class PosterizeFilterModel : FilterModel
    {
        public PosterizeFilterModel()
        {
            Name = "Poster";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new PosterizeFilter(8));

                return components;
            }
        }
    }

    public class SepiaFilterModel : FilterModel
    {
        public SepiaFilterModel()
        {
            Name = "Sepia";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new SepiaFilter());

                return components;
            }
        }
    }

    public class SharpnessFilterModel : FilterModel
    {
        public SharpnessFilterModel()
        {
            Name = "Finesse";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new SharpnessFilter(4));

                return components;
            }
        }
    }

    public class SketchFilterModel : FilterModel
    {
        public SketchFilterModel()
        {
            Name = "Croquis";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new SketchFilter(SketchMode.Color));

                return components;
            }
        }
    }

    public class SolarizeFilterModel : FilterModel
    {
        public SolarizeFilterModel()
        {
            Name = "Solarisé";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new SolarizeFilter(0.5f));

                return components;
            }
        }
    }

    public class StampFilterModel : FilterModel
    {
        public StampFilterModel()
        {
            Name = "Estampillé";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new StampFilter(3, 60));

                return components;
            }
        }
    }

    public class StepRotationLeftFilterModel : FilterModel
    {
        public StepRotationLeftFilterModel()
        {
            Name = "Rotation Gauche";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new RotationFilter(270));

                return components;
            }
        }
    }

    public class StepRotationRightFilterModel : FilterModel
    {
        public StepRotationRightFilterModel()
        {
            Name = "Rotation Droite";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new RotationFilter(90));

                return components;
            }
        }
    }

    public class WarpTwisterFilterModel : FilterModel
    {
        public WarpTwisterFilterModel()
        {
            Name = "Déformation";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new WarpFilter(WarpEffect.Twister, 0.3f));

                return components;
            }
        }
    }

    public class WatercolorFilterModel : FilterModel
    {
        public WatercolorFilterModel()
        {
            Name = "Watercolor";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new WatercolorFilter(0.8f, 0.5f));

                return components;
            }
        }
    }

    public class VignettingFilterModel : FilterModel
    {
        public VignettingFilterModel()
        {
            Name = "Vignette";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new VignettingFilter(0.4f, Windows.UI.Color.FromArgb(0xff, 0x00, 0x00, 0x00)));

                return components;
            }
        }
    }

#endregion

    #region Enhancement filters

    public class AutoEnhanceFilterModel : FilterModel
    {
        public AutoEnhanceFilterModel()
        {
            Name = "Amélioration Auto";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new AutoEnhanceFilter(true, true));

                return components;
            }
        }
    }

    public class AutoLevelsFilterModel : FilterModel
    {
        public AutoLevelsFilterModel()
        {
            Name = "Niveaux Auto";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new AutoLevelsFilter());

                return components;
            }
        }
    }

    public class ColorBoostFilterModel : FilterModel
    {
        public ColorBoostFilterModel()
        {
            Name = "Boost";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new ColorBoostFilter(3.0f));

                return components;
            }
        }
    }

    public class ExposureFilterModel : FilterModel
    {
        public ExposureFilterModel()
        {
            Name = "Exposition";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new ExposureFilter(ExposureMode.Natural, 0.9f));

                return components;
            }
        }
    }

    public class FoundationFilterModel : FilterModel
    {
        public FoundationFilterModel()
        {
            Name = "Fondation";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new FoundationFilter());

                return components;
            }
        }
    }

    public class LevelsFilterModel : FilterModel
    {
        public LevelsFilterModel()
        {
            Name = "Niveaux";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new LevelsFilter(0.7f, 0.2f, 0.4f));

                return components;
            }
        }
    }

    public class LocalBoostFilterModel : FilterModel
    {
        public LocalBoostFilterModel()
        {
            Name = "Boost Local";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new LocalBoostAutomaticFilter());

                return components;
            }
        }
    }

    public class TemperatureAndTintFilterModel : FilterModel
    {
        public TemperatureAndTintFilterModel()
        {
            Name = "Température & Teinte";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new TemperatureAndTintFilter(0.8, -0.4));

                return components;
            }
        }
    }

    public class WhiteboardEnhancementFilterModel : FilterModel
    {
        public WhiteboardEnhancementFilterModel()
        {
            Name = "Whiteboard";
        }

        [XmlIgnore]
        public override Queue<IFilter> Components
        {
            get
            {
                Queue<IFilter> components = new Queue<IFilter>();

                components.Enqueue(new WhiteboardEnhancementFilter(WhiteboardEnhancementMode.Soft));

                return components;
            }
        }
    }

    #endregion
}
