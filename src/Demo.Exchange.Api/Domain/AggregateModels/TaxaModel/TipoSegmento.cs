namespace Demo.Exchange.Domain.AggregateModel.TaxaModel
{
    using Demo.Exchange.Domain.SeedWorks;
    using System;
    using System.Linq;

    public class TipoSegmento : Enumeration
    {
        public TipoSegmento(string id, string name, string desricaoSimples)
            : base(id, name)
        {
            DesricaoSimples = desricaoSimples;
        }

        public static readonly TipoSegmento Varejo = new TipoSegmento("VAREJO", "Segmento para cliente do varejo", nameof(Varejo));
        public static readonly TipoSegmento Personnalite = new TipoSegmento("PERSONNALITE", "Segmento para cliente do varejo", nameof(Personnalite));
        public static readonly TipoSegmento Private = new TipoSegmento("PRIVATE", "Segmento para cliente do varejo", nameof(Private));

        public string DesricaoSimples { get; }

        public static TipoSegmento ObterPorId(string id)
            => GetAll<TipoSegmento>().SingleOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
    }
}