public class ContentAnime
{
    public TypeSprite TypeSprite { get; set; }
    public string NomSprite { get; set; }
    public string PathSprite { get; set; }
    public int NbFrame { get; set; }
    public ContentAnime(TypeSprite typeSprite, string nomSprite, string pathSprite, int nbFrame)
    {
        this.TypeSprite = typeSprite;
        this.NbFrame = nbFrame;
        this.NomSprite = nomSprite;
        this.PathSprite = pathSprite;
    }
}