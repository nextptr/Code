   M S B A M L   `   `   `  ����E  AGui.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Gui.Common
Gui.Common  5   	       Gui.Common.LoadingWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Gui.Common     5   	     � Title$  ProcessWindow��5   	   $	��600��6   $��20(�6,   $
��1000��6:   ���	#FF484848 � 
ResizeMode$ NoResize=�6^    � WindowStartupLocation$ CenterScreen=�6t   
����·�$��1q�5	       � 	Resources 5
       �� MergedDictionaries �� Source �� 
TargetType �� Property �� Value	 �� 
TargetType
 J� SnapsToDevicePixels � ColumnDefinitions �� Triggers �� Value �� Binding �� 
TargetName   ButtonS_Orange �� BasedOn �� Triggers P� Property P� Value �� Color  LabelS_Base�� 5   
    5      �� 5      $/ )/Gui.Common;component/Themes/Generic.xaml?�6%   %�  '��       '�� 6    7   '��     '�� �    7  �� 5      " ��6   .�5      ��  �@J�IsHitTestVisible .  65   �� 5       � ��
 �����6/   �� 5       � ��$ Center=�6=   �� 5       � �� 5      �� 5      "	 ��6*   .R�5      �� #��z��6%   #��z��6O   #��z��5   !   #
 z
 6U   ._�5   "   � #��z��6'   #��z��6f    5   &   �� 5   *   $��40 �6;   �� 5   *   .+�5   &   F� $��pathSuccess��6+   $	��5,0q�6>   $y�	Collapsed=�6K   "����    @KL �@KL ��� ����     	
��� � �$��2��5   +   #��z��6?   $��Center=�6~   F� 5   &   $��pathFail��6+   $	��5,0q�6;   $y�	Collapsed=�6H   +����        ���� ��� �    ��� ����     	
���  ��$��2��5    +   #��z��6?   $��Center=�6~   �� 5!   &   $��1��67   #��z��6G   5"   #   5#       5$      �� 5%   "   $ False��6I    6.   �� *Status+.a�5&   &   �� $ pathFail��6-    � y�$ Visible=�6Y   5'   #   �� 5(   "   $
 True��6I    6.   �� *Status+.a�5)   &   �� $ pathSuccess��6-    � y�$ Visible=�6\   5*   #   5,      5.      5/      �� 50      " ��6   8   .�51      ��  � ��$	 100��6*    52      P� 53       �@J�IsHitTestVisible .  . �54      ��  � �� 55      �� 56   "   $
 Gray��62   58      59      5;      �� 5<      " ��6   .�5=      ��  � ��
 ���� 6/   �� 5>       � ��
 ��·�60   �� 5?       � ��$	 0,1q�64   5@      �� 5A      " ��6   8   6'   5B      .��5D      �  � RowDefinitions 5E   
   �� 5F      $��50 �6   �� 5G      �� 5H      $
��Auto �6   .+�5J   
   �� -   5        $��lblState��5J      $!��正在初始化硬件……��6    �� 5K   
   -   5        $��lstProcesses��5K      $��1��6    �� DisplayMemberPath$ Description��63   A� 5L   
   $��2��6    A� Rows$ 1��6#   $��Center=�6,   $	��300��6I   $��50��6U   $
��0,10q�6a   .+�5M      �� -   5        $��btnGo��5M   ;   $��1��6    J� IsHitTestVisible .  $��进入��6H   �� 5N      -   5        $��btnExit��5N   ;   $��1��6    .  $��退出��6J   5O      5P      5Q      