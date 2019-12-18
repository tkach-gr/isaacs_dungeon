using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ECSIsaac.Components
{
    class DisplayComponent : Drawable
    {
        protected Sprite sprite;

        public Vector2f Position { get => sprite.Position; set => sprite.Position = value; }
        public float Rotation { get => sprite.Rotation; set => sprite.Rotation = value; }
        public Vector2f Origin { get => sprite.Origin; set => sprite.Origin = value; }
        public Vector2u Size => sprite.Texture.Size;
        public Vector2f Scale => sprite.Scale;
        
        public DisplayComponent(string fileName, Vector2f position = new Vector2f())
        {
            ComponentStorage<Drawable>.Components.Add(this);

            Texture texture = new Texture(ImageManager.Get(fileName));
            sprite = new Sprite(texture);
            sprite.Scale = Config.TextureScale;
            sprite.Position = position;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }

        public void UpdateTexture(string textureName)
        {
            sprite.Texture.Update(ImageManager.Get(textureName));
        }
    }
}
