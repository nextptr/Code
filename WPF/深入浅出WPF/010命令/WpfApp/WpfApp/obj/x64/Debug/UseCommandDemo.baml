   M S B A M L   `   `   `  ����A  =WpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WpfAppWpfApp  5          WpfApp.UseCommandDemo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:WpfApp     5        � 	Resources  5	       
  Delete �� Text��%*   &	        �� 5
   
   $ 删除��6)   5       J� InputBindings 5      �� 5   
    �� Gesture$ Ctrl+D��6    �� Command# [  6&   �� 5   
   $) #local:MyDeleteCommand.DeleteCommand��6   
 �� Key$ D��6C    �� 	Modifiers$
 Ctrlf�6K    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
      Command# [  6     5   
   -   5        $	 New��5      6N     5   
   -   5        $) #local:MyDeleteCommand.DeleteCommand��5      6~   .��5      A� 	 A� Columns$	 2��6   .+�5   
   �� $��基础用例��6   .��5      A� 
 A� Rows$
 2��6   .+�5      �� -   5        $��
stack_wrap��5       �� Orientation$ 
Horizontal=�6/   $��Center=�6H   $��Center=�6c   .+�5      �� -   5        $��txt_RecvCmd��5      $��45��61   $	��120��6=   $��Center=�6I   �� 5      -   5        $��btn_sendCmd��5      $��Send Command��60   5      A� 5      -   5        $��	unif_wrap��5   '   $
 2��6   .+�5      A� $	 3��6"   .+�5      �� -   5        $��btn_command_cut��5   !   $	��Cut��68   $	��Cut��6F   ��6T   ��  �� ElementName$ txt_copy_dst���� 5       -   5        $��btn_command_copy��5    !   $
��Copy��69   $
��Copy��6H   ��6W   �� $ txt_copy_dst���� 5!      -	   5        $��btn_command_past��5!   !   $
��Past��69   $��Paste��6H   ��6X   �� $ txt_copy_dst��5"      �� 5#      -
   5        $��txt_copy_dst��5#      $��拷贝数据目标��62   5$      5%      5&      A� 5'   
   $
 2��6   .+�5(      �� $��命令参数探究��6   .��5)      � $��6q�6    � RowDefinitions 5*      �� 5+      $��24 �6(   �� 5,      $��4 �6(   �� 5-      $��24 �6(   �� 5.      $��4 �6(   �� 5/      $��24 �6(   �� 50      $��4 �6(   �� 51      $��* �6(   .+�53      ]� .+�54      �� $��Name(命令源):��6$   $��Center=�66   $
��Left=�6Q   �� 55      -   5        $��txt_Name��55   "   $��Stretch=�63   56      �� 57      $��2��6   $��New Teacher��6*   $	��New��6@   $��Teacher��6N   �� 58      $��4��6   $��New Student��6*   $	��New��6@   $��Student��6N   �� 59      -   5        $��ls_NewItems��59   +   $��6��6   6+   5:      5;      �� 5<      $��Delete��6   .��5=      � $��6q�6    5>      �� 5?      $��24 �6(   �� 5@      $��24 �6(   �� 5A      $��24 �6(   �� 5B      $��* �6(   .+�5D      �� $��Delete��6   #��[  6.   ��6P   �� $ txt_command_dest���� 5E      $��1��6   $��MyDeleteCommand��6*   $)��#local:MyDeleteCommand.DeleteCommand��6D   ��6r   �� $ txt_command_dest���� 5F      -   5        $��txt_command_dest��5F   +   $��3��6   $��DELETE��6C   5G      5H      5I      5J      5K      