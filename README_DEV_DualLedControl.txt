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
>>> Linea 695+: "connectors" è la lista che appenderà alla comboBox <DisplayLedDisplayPanel.displayLedConnectorComboBox>;
allo stesso modo si può modificare (ora è fissa) <displayLedModuleSizeComboBox> (modello: SetConnectors(), ma invece che DataSource <= List<pins>, usare Items <= List(String))
e probabilmente nascondere le checkbox (se non lo fa già la <DisplayLedModuleSize_SelectedIndexChanged>)

UI\Panels\Output\DisplayLedDisplayPanel.cs
33  MobiFlight.UI.Panels.DisplayLedDisplayPanel.syncFromConfig(OutputConfigItem config)
Usa obj #2 ('<OutputConfigItem>.LedModule' -> <OutputConfig>.<LedModule>)
Linea 52: aggiungere setting esplicito lista <displayLedModuleSizeComboBox> in base a...?

[\UI\Dialogs\InputConfigWizard.cs]
(INPUT??????)
668 MobiFlight.UI.Dialogs.InputConfigWizard.
void displayLedAddressComboBox_SelectedIndexChanged(object sender, EventArgs e)
Usa obj #1
