# 🌸 Mochi Galaxy: Pastel Glow Shader (Shader Graph Guide)

Since I cannot directly manipulate the Unity Editor UI, here is the technical blueprint to create our "Aesthetic Pastel" shader using **Unity's Shader Graph** (URP).

## 🧩 Shader Graph Nodes & Connections

### 1. Base Clay Look (The "Mochi" Skin)
- **Node**: `Mesh Physical Material` (or use the Standard URP Lit graph).
- **Settings**:
    - **Base Color**: Connect to a `Color` property (Set to your Pastel Baby Blue/Lavender).
    - **Smoothness**: `0.4` (Gives it that soft, non-shiny clay look).
    - **Roughness**: `0.6`.

### 2. The "Aesthetic" Rim Glow (Fresnel Effect)
This creates the soft light around the edges of the character.
- **Node**: `Fresnel Effect`.
    - **Power**: Connect to a `Float` property (Default: `2.0`).
- **Node**: `Multiply`.
    - **Input A**: `Fresnel Effect` output.
    - **Input B**: Connect to a `Color` property (HDR) set to a bright white or light pink.
- **Connection**: Add this to your `Emission` input on the Master Stack.

### 3. The "Soft Blush" (Vertex Color or Mask)
- **Node**: `Texture 2D Asset`.
    - Use a small gradient circle mask for the cheeks.
- **Node**: `Lerp`.
    - **A**: Base Skin Color.
    - **B**: Blush Color property.
    - **T**: Your mask texture.

### 4. Vertex Animation (The "Idle Jiggle")
To give the mascot more life without complex rigs:
- **Node**: `Position` (Object Space).
- **Node**: `Sine` (Connect to `Time`).
- **Node**: `Add`.
    - Add a small Y-offset to the vertex position.
- **Connection**: Connect to `Vertex Position` in the Master Stack.

---

## 🎨 Recommended HDR Colors for Emission
For that "Neon Pastel" look, set your HDR Emission color to:
- **Intensity**: `+1.5`
- **Color**: `#FFD1FE` (Soft Pink Glow) or `#D1FAFF` (Aqua Glow).

## 🚀 How to Apply
1. In Unity, right-click `Create > Shader Graph > URP > Lit Shader Graph`.
2. Name it `MochiMaster`.
3. Build the nodes as described above.
4. Right-click the Graph > `Create Material`.
5. Drag that material onto your Mochi 3D model!
