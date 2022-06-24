using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
public class GamePlayInfo
{
    public List<Enemy> Enemys;
    public List<Entity> Entity;

    public Joueur J1;
    public Texture2D _GameBackground;
    public GamePlayInfo()
    {
        J1 = new Joueur("Golem", new Coordonnee(100, 100));
        J1.setTextures(TypeSprite.Walk_right, "Walk_right", "Golem01/Golem_01_Walking_right_001", 17);
        J1.setTextures(TypeSprite.Walk_left, "Walk_left", "Golem01/Golem_01_Walking_left_001", 17);
        J1.setTextures(TypeSprite.Attack_left, "Attack_left", "Golem01/Golem_01_Attacking_Left_000", 12);
        J1.setTextures(TypeSprite.Attack_right, "Attack_right", "Golem01/Golem_01_Attacking_Right_000", 12);
        J1.setTextures(TypeSprite.Dead_left, "Dead_left", "Golem01/Golem_01_Dying_Left_000", 15);
        J1.setTextures(TypeSprite.Dead_right, "Dead_right", "Golem01/Golem_01_Dying_Right_000", 15);
        J1.setTextures(TypeSprite.Idle_left, "Idle_left", "Golem01/Golem_01_Idle_Left_000", 12);
        J1.setTextures(TypeSprite.Idle_right, "Idle_right", "Golem01/Golem_01_Idle_Right_000", 12);
        _GameBackground = null;
        Enemys = new List<Enemy>();
    }
}