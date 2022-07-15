using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
public class GamePlayInfo
{
    public List<Enemy> Enemys { get; set; }
    public List<Entity> Entity { get; set; }
    public List<Region> Regions { get; set; }
    public Structure Castel { get; set; }
    public int Score { get; set; } = 0;
    public List<int> Scores { get; set; } = new List<int>();
    public bool pause { get; set; } = false;
    public Joueur J1 { get; set; }
    public Texture2D GameBackground { get; set; }
    public GamePlayInfo()
    {
        J1 = new Joueur("Knight", new Coordonnee(200, 200));
        J1.setTextures(TypeSprite.Walk_right, "Walk_right", "Player/Player_Walk_Right", 10);
        J1.setTextures(TypeSprite.Walk_left, "Walk_left", "Player/Player_Walk_Left", 10);
        J1.setTextures(TypeSprite.Attack_left, "Attack_left", "Player/Player_Attack_Left", 10);
        J1.setTextures(TypeSprite.Attack_right, "Attack_right", "Player/Player_Attack_Right", 10);
        J1.setTextures(TypeSprite.Dead_left, "Dead_left", "Player/Player_Dead_Left", 10);
        J1.setTextures(TypeSprite.Dead_right, "Dead_right", "Player/Player_Dead_Right", 10);
        J1.setTextures(TypeSprite.Idle_left, "Idle_left", "Player/Player_Idle_Left", 10);
        J1.setTextures(TypeSprite.Idle_right, "Idle_right", "Player/Player_Idle_Right", 10);
        GameBackground = null;
        J1.Pv = 100;
        J1.Damage = 2;
        J1.userName = "toto l'asticot";
        Entity = new List<Entity>();
        Entity.Add(J1);
        Regions = new List<Region>();
    }
}