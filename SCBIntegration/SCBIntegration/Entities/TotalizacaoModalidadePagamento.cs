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
    public partial class TotalizacaoModalidadePagamento
    {

        private uint codigoModalidadePagamentoField;

        private decimal valorArrecadadoField;

        /// <remarks/>
        public uint codigoModalidadePagamento
        {
            get
            { 
                return this.codigoModalidadePagamentoField;
            }
            set
            {
                this.codigoModalidadePagamentoField = value;
            }
        }

        /// <remarks/>
        public decimal valorArrecadado
        {
            get
            {
                return this.valorArrecadadoField;
            }
            set
            {
                this.valorArrecadadoField = value;
            }
        }
    }

}
