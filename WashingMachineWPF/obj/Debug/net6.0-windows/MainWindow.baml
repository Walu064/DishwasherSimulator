   M S B A M L   `   `   `  ����H  DDishwasherWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WashingMachineWPFWashingMachineWPF  5   	   "    WashingMachineWPF.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:WashingMachineWPF     5   	   -   5          � Title$  DishwasherSimulator��5   	   $	��613��6%   $	��906��62   .��5	      � $��0,0,0,5q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � 	Resources 5   
    �� 
TargetType �� Property �� Value �� 
StartPoint �� EndPoint � Color	 � Offset��%&  '�       '�� J     '�� T    '�� �    '�� �    �� 5      " �6   .�5      ��  � ��
 ��ح�6/   5      �� 5      " ��6   .�5      ��  � �� 5      �� 5      $	 0,0&�6.   $	 1,1&�6?   .��5      � $ Black��6+   $		 0.0Q�69   � 5      $ White��6+   $		 1.0Q�69   5      5      �� 5       � ��
 �����6/   5      �� 5       " ��6   .�5!      ��  � ��
 �����6/   �� 5"       � ��$
 Bold"�6/   5#      �� 5$      " ��6   .�5%      ��  � ��
 �   �6/   �� 5&       � ��
 �����6/   �� 5'       � ��$
 Bold"�6/   �� 5(       � ��$ Consolas)�6/   5)      �� 5*      " ��6   .�5+      ��  � ��
 �����6/   �� 5,       � ��$
 Bold"�6/   5-      5.      .+�50   
   �� $��0��6   $��10q�6%   ���	#FF000000.+�51      �� -   5        
 ��Name$
 displayTextBox��51       �� 
IsReadOnly$
 True��6.   
���   �$��1q�6T   ���	#FF080101
�������$��Consolas)�52   ;   $��18(�6Q   $��Center=�53      $
��Left=�63   $	��100��54      $	��320��6   $��5q�6*   $��	0,10,0,10q�66   �� 55      -   5         ��Name$ buttonStart��55      $��Rozpocznij zmywanie��6*   $��40��6]   $	��320��6i   $��	ExtraBold"�6u   �� 56      -   5        $ 
buttonStop��56      $��Zakończ zmywanie��6)   $��40��6W   $	��320��6c   $��	ExtraBold"�6o   �� 57      -   5        $ buttonProgram��57      $��Wybierz program��6,   $��40��6`   $	��320��6l   $��	ExtraBold"�6x   �� 58      -   5        $ buttonOpenDoor��58      $��Zamknij drzwi��6-   $��40��6W   $	��320��6c   $��	ExtraBold"�6o   �� 59      -   5        $ buttonInsertDish��59      $��Włóż naczynia��6/   $��41��6Z   $	��320��6f   $��	ExtraBold"�6r   �� 5:      -   5        $ buttonEmpty��5:      $��Wyciągnij naczynia��6*   $��39��6\   $	��320��6h   $��	ExtraBold"�6t   �� 5;      $��ODCZYTY SENSORÓW��6   $��0,20,0,0q�6/   �� 5<       �� Orientation$ 
Horizontal=�6   $
��0,10q�62   .+�5=      � -	   5         �Name$ progressBarTime��5=       � Maximum$ 60Q�6d   $��22��6q   $	��100��6}    � Orientation$ 
Horizontal=�6�    � Value 67   �� *Text+ �� ElementName$ washTime��6�   �� 5>      -
   5         ��Name$ washTime��5>      $��0��6.   $��20(�67   $
��Bold"�6E   $��40��6W    �� TextAlignment$ Center=�6b   $��Center=�6y   $
��10,0q�6�   �� 5?      $��Czas [min.]��6   $	��110��6/   $��Center=�6;   5@      �� 5A      $ 
Horizontal=�6   $
��0,10q�62   .+�5B      � -   5        $ progressBarWater��5B      $ 10Q�6g   $��22��6t   $	��100��6�   $ 
Horizontal=�6�    68   �� *Text+$ 
waterLevel��6�   �� 5C      -   5        $ 
waterLevel��5C      $��0��60   $��20(�69   $
��Bold"�6G   $��40��6Y   $ Center=�6d   $��Center=�6{   $
��10,0q�6�   �� 5D      $��Ilość wody [ l ]��6   $	��110��64   $��Center=�6@   5E      �� 5F      $ 
Horizontal=�6   $
��0,10q�62   .+�5G      � -   5        $ progressBarTemperature��5G      $ 30Q�6r   $��22��6   $	��100��6�   $ 
Horizontal=�6�    6>   �� *Text+$ temperatureText��6�   �� 5H      -   5        $ temperatureText��5H      $��20��65   $��20(�6?   $
��Bold"�6M   $��40��6_   $ Center=�6j   $��Center=�6�   $
��10,0q�6�   �� 5I      $��Temperatura [ °С ]��6   $	��110��66   $��Center=�6B   5J      5K      �� 5N   
   -   5         ��Name$ imageDoorOpened��5N      $��1��6)   $��/dishwasher_opened.jpg��69   $
��Fill=�6Y   $��
0,10,10,10q�6h   $y�Visible=�6|   �� 5O   
   -   5        $ imageDoorClosed��5O      $��1��6)   $��/dishwasher_closed.jpg��69   $
��Fill=�6Y   $��
0,10,10,10q�6h   $y�Hidden=�6|   5P      5Q      