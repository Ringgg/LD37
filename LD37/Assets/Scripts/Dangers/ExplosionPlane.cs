using UnityEngine;

public class ExplosionPlane : AreaDanger
{
    public ExplosionPlane secondPlane;

    public override bool IsInDanger(Hero hero)
    {
        return heroesIn.Contains(hero);
    }

    public override Vector3 GetEscapePosition(Hero hero)
    {
        Vector3 dest;
        if (secondPlane.isActiveAndEnabled)
        {
            dest = -(Vector3.zero - hero.transform.position);
        }
        else
        {
            dest = -(transform.position - hero.transform.position);
        }
        dest.y = hero.transform.position.y;
        return hero.transform.position + dest.normalized;
    }

    void OnDestroy()
    {
        GiveHeroesDamage();
    }
}
