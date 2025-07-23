# HostLookup

## Beschreibung

**HostLookup** ist eine Windows Forms Anwendung, die speziell für das Kundenprojekt KDO entwickelt wurde. Das Programm liest Hostdaten (Hostname, MAC-Adresse, UUID) aus einer Excel-Datei ein, filtert gezielt nach eingegebenen Hostnamen und exportiert die Ergebnisse in eine standardisierte CSV-Datei.

---

## Funktionen

- Excel-Dateien (.xlsx, .xls, .xlsm) einlesen  
- Hostnamen mehrfach eingeben und filtern  
- Fortschrittsanzeige beim Laden der Excel-Datei  
- Log-Ausgabe mit Erfolgs- und Fehlerhinweisen  
- Export der gefilterten Daten in CSV mit definiertem Format  
- GUI mit Fade-in-Effekt und optionalem Start-Sound  
- Saubere Freigabe der Excel COM-Objekte  

---

## Voraussetzungen

- Windows-Betriebssystem  
- Microsoft Excel (für Excel Interop)  
- .NET Framework (WinForms-kompatibel)  

---

## Nutzung

1. Programm starten  
2. Excel-Datei mit Hostdaten laden  
3. Hostnamen in das Eingabefeld eintragen (kommagetrennt oder Zeilenweise)  
4. Auf „Hinzufügen“ klicken, um die Daten zu filtern und ins Log aufzunehmen  
5. Nach dem Hinzufügen mehrerer Hostnamen auf „CSV exportieren“ klicken  
6. Speicherort und Dateinamen wählen und CSV speichern  

---

## CSV-Format
Computer.Computername;Computer.Domäne;Computer.Domäne J/N;Computer.MAC-Adresse;Computer.UUID;Computer.PXE fähig
0005-BLABLA;CAP;1;84BA33C13E30;DE6626CC-2F33-1133-A85C-9A18A6D11333;1


---

## Lizenz

Dieses Projekt ist unter der MIT Lizenz lizenziert. (https://opensource.org/license/MIT)
Du kannst den Code frei verwenden, kopieren, ändern und weiterverbreiten – auch kommerziell – unter der Bedingung, dass der ursprüngliche Urheber genannt wird.

---



