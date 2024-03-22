# QRCodeGenerator

## 2024-03-22 - VERSION FINALE
### Description
Cette version de QrCodeGenerator propose maintenant un interface graphique à l'utilisateur.
De plus, il est maintenant possible de personnaliser le code QR en y ajoutant un logo central et en changeant les couleurs d'arrière plan et de modules.
#### NOTES : 
- Certaines combinaisons de couleurs peuvent rendre le code QR illisible.
- L'ajout d'un logo central force le niveau de correction d'erreur : H


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
   
# _____________________________________________________


## 2024-02-02 - PREUVE DE CONCEPT (v1)
Cette versions permet de générer un code QR en console (version 1-11) vers un fichier .png 

### La version 1 permet de :
    
    - Recevoir le texte à encoder entré par l'utilisateur.
    - Le système choisit automatiquement le type d'encodage, le meilleur masque et la version selon le message
    - Le logiciel supporte aussi l'entrée manuelle de la version, du niveau de correction d'erreur et du type d'encodage (dans le code)
    - Le logiciel génère une image sous format .png contenant le code QR (version 11 ou inférieure)
    
    * Encodages fonctionnels : TOUS         - automatique ou manuel  
    * Masques fonctionnels : TOUS           - automatique seulement  
    * Versions fonctionnelles : 1-11        - automatique ou manuel  
    * Niveaux correction d'erreur : TOUS    - manuel seulement  

### TODO List (par ordre de priorité)  |  [X] = FAIT :

    - Faire fonctionner Versions 11-40 ??? (Priorité à confirmer) [X]
    - Interface graphique [X]
    - Ajout de logo [X]
    - Taille de l'image de sortie [X]
    - Documentation
    - README



   




