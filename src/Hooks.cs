using Noise;
using UnityEngine;

namespace Boom;

public sealed class Hooks
{
    public Hooks()
    {
        On.Weapon.ChangeMode += Weapon_ChangeMode;
    }

    private void Weapon_ChangeMode(On.Weapon.orig_ChangeMode orig, Weapon self, Weapon.Mode newMode)
    {
        if (self.mode == Weapon.Mode.Thrown && self is Rock) {
            Vector2 pos = Vector2.Lerp(self.firstChunk.pos, self.firstChunk.lastPos, 0.35f);

            self.room.AddObject(new SootMark(self.room, pos, 80f, true));
            self.room.AddObject(new Explosion(self.room, self, pos, 7, 250f, 6.2f, 2f, 280f, 0.25f, self.thrownBy, 0.7f, 160f, 1f));
            self.room.AddObject(new Explosion.ExplosionLight(pos, 280f, 1f, 7, new(1f, 0.4f, 0.3f)));
            self.room.AddObject(new Explosion.ExplosionLight(pos, 230f, 1f, 3, new(1f, 1f, 1f)));
            self.room.AddObject(new ExplosionSpikes(self.room, pos, 14, 30f, 9f, 7f, 170f, new(1f, 0.4f, 0.3f)));
            self.room.AddObject(new ShockWave(pos, 330f, 0.045f, 5));

            self.room.PlaySound(SoundID.Bomb_Explode, pos);
            self.room.InGameNoise(new InGameNoise(pos, 9000f, self, 1f));

            self.room.ScreenMovement(pos, default, 1.3f);

            self.abstractPhysicalObject.LoseAllStuckObjects();
            self.Destroy();

            return;
        }

        orig(self, newMode);
    }
}
