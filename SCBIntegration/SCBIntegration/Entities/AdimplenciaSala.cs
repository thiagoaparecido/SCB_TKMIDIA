using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBIntegration.Entities
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AdimplenciaSala
    {

        private uint registroANCINESalaField;

        private string situacaoSalaField;
         
        /// <remarks/>
        public uint registroANCINESala
        {
            get
            {
                return this.registroANCINESalaField;
            }
            set
            {
                this.registroANCINESalaField = value;
            }
        }

        /// <remarks/>
        public string situacaoSala
        {
            get
            {
                return this.situacaoSalaField;
            }
            set
            {
                this.situacaoSalaField = value;
            }
        }
    }
}
