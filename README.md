# Map Expand - Mod pour R.E.P.O.

Ce mod ajoute une vue élargie de la carte du jeu R.E.P.O.

## Prérequis

- [R.E.P.O.](https://store.steampowered.com/app/3241660/REPO/)
- [BepInEx 5.4.21](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21)

## Installation

1. Téléchargez et installez BepInEx 5.4.21 dans votre dossier de jeu R.E.P.O.
   - Utilisez le script `setup_bepinex.bat` pour l'installer automatiquement
   - Ou extrayez manuellement le contenu de BepInEx dans le dossier principal du jeu où se trouve REPO.exe
   - Lancez le jeu une fois pour que BepInEx crée ses dossiers de configuration

2. Téléchargez la dernière version du mod depuis la page des releases
   - Extrayez le fichier REPOMod.dll dans le dossier `BepInEx/plugins/`
   - Ou compilez le mod vous-même avec `build.bat`

3. Lancez le jeu et profitez des nouvelles fonctionnalités!

## Fonctionnalités

- **Vue Élargie de la Carte**: Augmente la taille de la caméra de la carte ("Dirt Finder Map Camera") à 5, permettant de voir une bien plus grande partie de la carte sans avoir à faire défiler autant.
- Cette modification n'affecte que la caméra de la carte dans la scène principale du jeu.

## Configuration

Après avoir lancé le jeu une fois avec le mod, un fichier de configuration est créé dans:
`BepInEx/config/com.ranily.repo.mapexpand.cfg`

Vous pouvez modifier les paramètres suivants:
- `MapCamera.Size`: Taille de la caméra de la carte (défaut: 10, valeur du jeu: ~5)

## Ressources pour le développement

### Outils essentiels à télécharger
- [BepInEx 5.4.21](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21) - Framework de modding
- [dnSpy](https://github.com/dnSpy/dnSpy/releases) ou [ILSpy](https://github.com/icsharpcode/ILSpy/releases) - Pour décompiler et explorer le code du jeu
- [Unity Explorer](https://github.com/sinai-dev/UnityExplorer/releases) - Pour explorer les objets Unity pendant l'exécution du jeu

### Termes de recherche pour GitHub
Pour trouver d'autres mods existants, recherchez:
- "REPO game mod"
- "REPO BepInEx"
- "R.E.P.O modding"

### Communautés à rejoindre
- [Discord officiel de R.E.P.O](https://discord.gg/semiwork) - Vérifiez s'il existe un canal dédié au modding
- Recherchez des communautés sur Reddit ou Nexus Mods

## Compilation

Si vous souhaitez compiler le mod vous-même:

1. Assurez-vous d'avoir installé le SDK .NET 6.0 ou supérieur
2. Clonez ce dépôt
3. Modifiez le chemin du jeu dans build.bat si nécessaire
4. Exécutez build.bat pour compiler le mod
5. Le DLL compilé sera copié dans le dossier BepInEx du jeu

## Dépannage

- Si le mod ne se charge pas, vérifiez les fichiers logs dans `BepInEx/logs/`
- Si la carte n'est pas agrandie après l'installation du mod:
  - Appuyez sur F8 en jeu pour forcer l'agrandissement de la carte
  - Vérifiez que la valeur `MapCamera.Size` dans le fichier de configuration n'est pas trop petite
  - Vérifiez les logs pour voir si la caméra a été correctement identifiée
- Assurez-vous que la version de BepInEx est compatible (5.4.21 recommandée)

## Configuration de l'environnement de développement

### Installation de BepInEx pour le développement

1. **Téléchargez BepInEx 5.4.21**:
   - Rendez-vous sur [la page des releases de BepInEx](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21)
   - Téléchargez `BepInEx_x64_5.4.21.0.zip` pour les jeux Windows 64-bit

2. **Installation dans le jeu**:
   - Extrayez tout le contenu du ZIP dans le dossier principal du jeu R.E.P.O. (où se trouve REPO.exe)
   - Lancez le jeu une fois puis fermez-le pour que BepInEx crée ses dossiers de configuration

3. **Configuration pour le développement**:
   - Créez un dossier `libs` dans votre dossier de développement (`c:\Users\raphi\Downloads\Fullmap REPO\libs`)
   - Copiez les fichiers suivants depuis le dossier d'installation de BepInEx vers votre dossier `libs`:
     - `BepInEx/core/BepInEx.dll`
     - `BepInEx/core/0Harmony.dll`
     - `BepInEx/core/MonoMod.Utils.dll`
     - `BepInEx/core/MonoMod.RuntimeDetour.dll`

4. **Référencer les DLLs dans votre projet**:
   - Ajoutez des références à ces DLLs dans votre fichier .csproj
   - Votre fichier .csproj devrait déjà contenir ces références via les packages NuGet

### Organisation des fichiers

Structure recommandée pour votre projet:
```
c:\Users\raphi\Downloads\Fullmap REPO\
├── REPOMod.csproj          # Fichier projet
├── build.bat               # Script de compilation
├── README.md               # Documentation
├── libs\                   # DLLs externes pour référence (si nécessaire)
├── lib\                    # DLLs du jeu pour référence
│   ├── Assembly-CSharp.dll
│   ├── UnityEngine.dll
│   └── UnityEngine.CoreModule.dll
└── src\                    # Code source du mod
    ├── Plugin.cs           # Point d'entrée du mod
    ├── PluginInfo.cs       # Informations sur le mod
    └── Utils\              # Classes utilitaires
        └── GameExplorer.cs
```

## Crédits

- Développé par Ranily
- Basé sur le jeu R.E.P.O. par semiwork
