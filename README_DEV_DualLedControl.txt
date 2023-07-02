#### Objects

### 1 ### Phys. Device config

[MobiFlight\Config\LedModule.cs]
9 MobiFlight.Config.LedModule(:BaseDevice)    

### 2 ### Output target config
[MobiFlight\OutputConfig\LedModule.cs]
12 MobiFlight.OutputConfig.LedModule          

### 3 ### Command device
[MobiFlight\MobiFlightLedModule.cs]           
15 MobiFlight.MobiFlightLedModule(:IConnectedDevice) 
>>>>> Prende dati da device fisici #1 in MobiFlight.MobiFlightModule.LoadConfig (line 307)
>>>>> in linee 348-352


#### Functions

UI\Panels\OutputWizard\DisplayPanel.cs
114 MobiFlight.UI.Panels.OutputWizard.syncFromConfig(OutputConfigItem cfg)
Usa obj #2 -> TRAMITE <DisplayLedDisplayPanel>
586: assegna evento cambio unità [DisplayLedDisplayPanel.OnLedAddressChanged] <= displayLedAddressComboBox_SelectedIndexChanged
>>> displayLedAddressComboBox_SelectedIndexChanged: da linea 688 recupera dati da device tipo #3

UI\Panels\Output\DisplayLedDisplayPanel.cs
33  MobiFlight.UI.Panels.DisplayLedDisplayPanel.syncFromConfig(OutputConfigItem config)
Usa obj #2 ('<OutputConfigItem>.LedModule' -> <OutputConfig>.<LedModule>)

[\UI\Dialogs\InputConfigWizard.cs]
(INPUT??????)
668 MobiFlight.UI.Dialogs.InputConfigWizard.
void displayLedAddressComboBox_SelectedIndexChanged(object sender, EventArgs e)
Usa obj #1
