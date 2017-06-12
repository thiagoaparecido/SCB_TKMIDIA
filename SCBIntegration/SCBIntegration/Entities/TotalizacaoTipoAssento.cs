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
    public partial class TotalizacaoTipoAssento
    {

        private string codigoTipoAssentoField;

        private uint quantidadeDisponibilizadaField;

        private TotalizacaoCategoriaIngresso[] totalizacoesCategoriaIngressoField;

        /// <remarks/>
        public string codigoTipoAssento
        {
            get
            {
                return this.codigoTipoAssentoField;
            }
            set
            {
                this.codigoTipoAssentoField = value;
            }
        }

        /// <remarks/>
        public uint quantidadeDisponibilizada
        {
            get
            {
                return this.quantidadeDisponibilizadaField;
            }
            set
            {
                this.quantidadeDisponibilizadaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("totalizacaoCategoriaIngresso", IsNullable = false)]
        public TotalizacaoCategoriaIngresso[] totalizacoesCategoriaIngresso
        {
            get
            {
                return this.totalizacoesCategoriaIngressoField; 
            }
            set
            {
                this.totalizacoesCategoriaIngressoField = value;
            }
        }
    }
}
