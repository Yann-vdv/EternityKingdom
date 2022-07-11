using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using projet.Models;
using System;

public class Characters : Entity
{
    public Coordonnee Co { get; set; }
    public Boolean IsDead { get; set; } = false;
    public Dictionary<TypeSprite, ContentAnime> Textures { get; set; } = new Dictionary<TypeSprite, ContentAnime>();
    public Dictionary<string, Animation> LoadAnimation { get; set; }
    public int PvMax { get; set; } = 5;
    public Colision Colision { get; set; }
    public int Pv { get; set; } = 2;
    public int Damage { get; set; } = 1;
    public Vector2 Velocity { get; set; } = new Vector2(0, 0);
    private static int createdCount = 0;
    public int Id { get; private set; }

    public Characters(Coordonnee c)
    {
        Co = c;
        Id = createdCount;
        createdCount++;
    }
    public void setTextures(TypeSprite typeSprite, string nomSprite, string sprite, int nbFrame)
    {
        Textures.Add(typeSprite, new ContentAnime(typeSprite, nomSprite, sprite, nbFrame));
    }
    // public string setName(TypeSprite typeSprite)
    // {
    //     return $"{typeSprite}_{this.Id}";
    // }
    public void TakeDamage(Characters c, int damage)
    {
        if (c.Pv - damage > 0)
        {
            c.Pv = c.Pv - damage;
        }
        else
        {
            c.Pv = 0;
            c.IsDead = true;
        }
    }
    public void Attack()
    {
        List<Enemy> listEnemy = this.Colision.WhoIn();
        foreach (Enemy enemy in listEnemy)
        {
            TakeDamage(enemy, Damage);
        }
    }

}