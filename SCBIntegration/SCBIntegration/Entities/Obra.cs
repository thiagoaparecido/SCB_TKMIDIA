using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBIntegration.Entities
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Obra
    {

        private string numeroObraField;

        private string tituloObraField;

        private string tipoTelaField;

        private string digitalField;

        private uint tipoProjecaoField;

        private string audioField;

        private string legendaField;

        private string librasField;

        private string legendagemDescritivaField;

        private string audioDescricaoField;

        private Distribuidor distribuidorField;

        /// <remarks/>
        public string numeroObra
        {
            get
            {
                return this.numeroObraField;
            }
            set
            {
                this.numeroObraField = value;
            }
        }

        /// <remarks/>
        public string tituloObra
        {
            get
            {
                return this.tituloObraField;
            }
            set
            {
                this.tituloObraField = value;
            }
        }

        /// <remarks/>
        public string tipoTela
        {
            get
            {
                return this.tipoTelaField;
            }
            set
            {
                this.tipoTelaField = value;
            }
        }

        /// <remarks/>
        public string digital
        {
            get
            {
                return this.digitalField;
            }
            set
            {
                this.digitalField = value;
            }
        }

        /// <remarks/>
        public uint tipoProjecao 
        {
            get
            {
                return this.tipoProjecaoField;
            }
            set
            {
                this.tipoProjecaoField = value;
            }
        }

        /// <remarks/>
        public string audio
        {
            get
            {
                return this.audioField;
            }
            set
            {
                this.audioField = value;
            }
        }

        /// <remarks/>
        public string legenda
        {
            get
            {
                return this.legendaField;
            }
            set
            {
                this.legendaField = value;
            }
        }

        /// <remarks/>
        public string libras
        {
            get
            {
                return this.librasField;
            }
            set
            {
                this.librasField = value;
            }
        }

        /// <remarks/>
        public string legendagemDescritiva
        {
            get
            {
                return this.legendagemDescritivaField;
            }
            set
            {
                this.legendagemDescritivaField = value;
            }
        }

        /// <remarks/>
        public string audioDescricao
        {
            get
            {
                return this.audioDescricaoField;
            }
            set
            {
                this.audioDescricaoField = value;
            }
        }

        /// <remarks/>
        public Distribuidor distribuidor
        {
            get
            {
                return this.distribuidorField;
            }
            set
            {
                this.distribuidorField = value;
            }
        }
    }
}
