# QRCodeGenerator
Jeremy Guerin & Olivier Bergeron-Houde


## 2024-03-22 - VERSION FINALE
### Description
Cette version de QrCodeGenerator propose maintenant un interface graphique à l'utilisateur.
L'utilisateur peut choisir tous les paramètres de génération, ou laisser l'application choisir les meilleurs paramètres.
Un message d'erreur sera retourné en cas de paramètres incompatibles.
De plus, il est maintenant possible de personnaliser le code QR en y ajoutant un logo central et en changeant les couleurs d'arrière plan et de modules.
#### NOTES : 
      - Certaines combinaisons de couleurs peuvent rendre le code QR illisible.
      - L'ajout d'un logo central force le niveau de correction d'erreur H.

### Installation
- Télécharger le Windows-Build du dernier release
- Extraire le contenu
- Lancer QRGenerator_Interface.exe pour utilisation avec l'interface graphique
- Lancer QRGenerator.exe pour utilisation console avec lignes de commandes


### Utilisation
#### Fenêtre de génération
-  L'utilisateur entre son texte à convertir en code QR.
-  Il choisit ses paramètres (ou coche la case "Auto" pour que l'application sélectionne les meilleurs paramètres à sa place).
-  Si l'utilisateur choisit lui même et qu'un des paramètres cause des problèmes de génération, une erreur est retournée pour demander à l'utilisateur de changer ce paramètre.
-  L'utilisateur choisit un chemin ou enregistrer son fichier (png).
-  L'utilisateur choisit un path ou enregistrer son fichier.
-  NOTE : Si un fichier de même nom existe à cet endroit, il sera remplacé par le nouveau code QR.
-  Une fois le code QR généré et exporté, le bouton pour ouvrir la fenêtre "personnalisation" est maintenant clickable.

#### Fenêtre de personnalisation - PERSONNALISE LE DERNIER CODE QR GÉNÉRÉ
-  L'utilisateur clique sur le bouton "personnaliser" pour ouvrir la fenêtre de personnalisation
-  L'utilisateur importe un logo à mettre au centre de son code qr
-  L'utilisateur choisis les couleurs des modules et de fond
-  Le bouton "OK" génère un nouveau fichier.png qui sera nommé : [NomDuCodeQr]-custom-[dateheure].png, l'utilisateur peut donc faire plusieurs personnalisations du même code QR sans que la nouvelle écrase la précédente.
-  Le bouton "Fermer" ferme la fenêtre de personnalisation. (peut être fermée sans avoir effectué de personnalisation)


                     
## DOCUMENTATION :
https://www.thonky.com/qr-code-tutorial/


# Application console :

## Documentation
```
Usage: QRGenerator [-t text] [-s scale] [-p path] [-pc patternColor] [-l logoPath] [-ls logoShadowType] [-bc backgroundColor] [-e errorCorrectionLevel] [-v version] [-em encodingMode] [-m mask] [-h]
Options:
  -t text: Text to encode
  -s scale: Scale of the QRCode output image
  -p path: Path to save the QRCode
  -pc patternColor: HTML color code of the pattern
  -l logoPath: Path to the logo
  -ls logoShadowType: Type of shadow for the logo (circle, shadow, cicle+shadow)
  -bc backgroundColor: HTML color code for the Background
  -e errorCorrectionLevel: Error correction level (L, M, Q, H)
  -v version: Version of the QRCode
  -em encodingMode: Encoding mode (Numeric, Alphanumeric, Byte)
  -m mask: Mask to apply
  -h: Display this help message
  --version: Display the version
```

## Example d'utilisation :

### Utilisation de base
```
QRGenerator -t "Hello World" -p qrcode.png
```

### Avec couleur de fond et de module
```
QRGenerator -t "Hello World" -s 10 -p qrcode.png -pc #FF0000 -bc #ffffcf 
```

### Avec logo
```
QRGenerator -t "Hello World" -s 20 -p qrcode.png -pc #FF0000 -bc #ffffcf -e H -l logo.png -ls circle+shadow
```

### Avec tout les paramètres
```
QRGenerator -t "Hello World" -s 10 -p qrcode.png -pc #FF0000 -bc #ffffcf -e H -v 5 -em Byte -m 1 -l logo.png -ls circle+shadow
```

