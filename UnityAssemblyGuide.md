# 🧸 Building your Mochi Mascot in Unity

Since we are starting from scratch, here is how to "assemble" your first 3D Mascot using the code I've provided.

## 1. Create the Mascot Body
In your Unity Hierarchy:
1. **Right-click > 3D Object > Capsule**. Name it `MochiBody`.
2. **Body Prep**: In the Inspector, set Scale to `(1, 0.8, 1)` to give it that "chibi" squashed look.
3. **Add Head**: Right-click on `MochiBody` > **3D Object > Sphere**. Name it `Head`. Set Position to `(0, 1.2, 0)` and Scale to `(1.2, 1.2, 1.2)`.

## 2. Attach the "Brain" (Scripts)
1. Select the top-most object (`MochiBody`).
2. **Add Component > Character Controller**.
3. **Add Component > Mochi Character Controller**.
4. Drag your `MochiModelSettings` asset into the reference slot (if you created one).

## 3. Make it Multiplay-Ready
1. **Add Component > Network Object**. This tells Photon "This mascot belongs to a player!"
2. **Add Component > Network Transform**. This syncs your movement with other players.
   - *Note: Check "Sync Scale" and "Sync Rotation".*

## 4. Setup the Scene
1. Create a new empty GameObject in your Scene. Name it `NetworkManager`.
2. **Add Component > Mochi Network Manager**.
3. **Prefab it**: Drag your `MochiBody` from the Hierarchy into your Project tab to turn it into a Template (Prefab).
4. **Link it**: Select `NetworkManager` and drag your new Mochi Prefab into the `Player Prefab` slot in the Inspector.

---

## 🎨 Aesthetic Tip
To get the **Pastel Dream** lighting look:
1. Go to `Window > Rendering > Lighting`.
2. Environment Tab: Set **Skybox Material** to a `New Material` using a **Skybox/Gradient** shader (Peach to Lavender).
3. **Ambient Light**: Set to a soft pink or baby blue instead of gray.
