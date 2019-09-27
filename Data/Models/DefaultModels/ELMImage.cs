using Data.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.DefaultModels
{
    public class ELMImage : IdBase
    {
        public string ImageFullNameLarge { get; set; }
        public string ImageFullNameMedium { get; set; }
        public string ImageFullNameSmall { get; set; }

        public string ImagePathLarge { get; set; }
        public string ImagePathMedium { get; set; }
        public string ImagePathSmall { get; set; }

        public string Large { get => ImagePathLarge + "/" + ImageFullNameLarge; }
        public string Medium { get => ImagePathMedium + "/" + ImageFullNameMedium; }
        public string Small { get => ImagePathSmall + "/" + ImageFullNameSmall; }

        public string Name { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
