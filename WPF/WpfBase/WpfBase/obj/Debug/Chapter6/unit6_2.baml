   M S B A M L   `   `   `  ����B  >WpfBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfBase.Chapter6WpfBase.Chapter6  5      /clr-namespace:WpfBase.CommonWpfBase.Common  5          WpfBase.Chapter6.unit6_2   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:WpfBase.Chapter6     5      /commonclr-namespace:WpfBase.Common     5      .��5
      ��   �� TabStripPlacement$  Bottom=�6   .@�5   
   �� ��5      �� 5      $��按钮��6   $��17(�6%   $
��Bold"�63   .��5      �  � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �  � RowDefinitions 5      �� 5      $��* �6(   �� 5      $��* �6(   �� 5      $��* �6(   �� 5      $��* �6(   �� 5      $��* �6(   �� 5      $��* �6(   �� 5      $��* �6(   �� 5      $��* �6(   .+�5      �� -   5        $��	btn_Hover��5   *   $��0��6   $��HoverButton��6;    �� 	ClickMode$ Hover=�6Q   �� 5       -   5        $��
btn_Repeat��5    0   $��1��6#   $��RepeatButton��6B   A� 5!       A� Columns$ 5��6"    A� Rows$ 2��6.   $��2��67   .+�5"      	� -   5        $��RadioBtn��5"   &    	� 	GroupName$ 1��69   6G   	� 5#      -   5        $��RadioBtn��5#   &   $ 1��69   6G   	� 5$      -   5        $��RadioBtn��5$   &   $ 2��69   6G   	� 5%      -   5        $��RadioBtn��5%   &   $ 2��69   6G   	� 5&      -   5        $��RadioBtn��5&   &   $ 2��69   6G   	� 5'      -   5        $��RadioBtn��5'   &   $ 1��69   6G   	� 5(      -	   5        $��RadioBtn��5(   &   $ 1��69   6G   	� 5)      -
   5        $��RadioBtn��5)   &   $ 3��69   6G   	� 5*      -   5        $��RadioBtn��5*   &   $ 3��69   6G   	� 5+      -   5        $��RadioBtn��5+   &   $ 3��69   6G   5,      �� 5-      -   5        $��CheckBox��5-      $��3��62   $��Center=�6?   6Z   �� 5.      $��4��6   $��
ToolTipBtn��6*    � ToolTip$ This is my toolTip��6?   �� 5/      $��5��6   $��嵌套ToolTipBtn��6*    50      _� 51      ���	#60AA4030
������� _� HasDropShadow . .��52   "   A� $ 2��6.   .+�53   &   �� $��Label and Image��6,   �� 54   &   $��/Images/game.png��6,   55   #   56      58      �� 59      -   5        $��	btnUpDown��59   ?   $��6��6   $��UpDown事件测试��6*   6?   5:      %   WpfBase.Common.ListMessagePanel  5;      -   5        	  Name$	 ls_event��5;   :   $��1��6*   6:   5<      5>      �� 5?   
   ��5@      �� 5A      $��Popup控件��6   $��17(�6*   $
��Bold"�68   .��5C      A� $ 2��6   .+�5D      � .+�5E      �� $
��Wrap=�6    ��The Project Gutenberg EBook of Romeo and Juliet, by William Shakespeare This eBook is for the use of anyone anywhere at no cost and with �� 5F   C   -   5        
 �� TextDecorations$
 	UnderLine{�5F   G   
���  ��.�6�   almost6�    no restrictions whatsoever."5G       � 5H      -   5        $��popLink��5H       � 	StaysOpen .  $��Mouse=�6=   $	��200��6O   $��Slide=�6^    � AllowsTransparency . .��5I      �� 
�������$��2q�65   
�������._�5J      �� $��10q�6(   $
��Wrap=�64   For more information,see �� 5K   7   -   5        $9��3https://fanyi.baidu.com/?aldtype=16047#en/zh/almost?�5K   A   .�6�   百度6�   5L      5M      5N      5O      � 5P      .+�5Q      �� -   5        $��
Show Popup��5Q      $��20(�61   
��� ���$	��Top=�6S   $��Center=�6k   6�   � 5R      -   5        $��popWin��5R      $��Mouse=�6*   $	��200��6<   $
��Fade=�6K    . .��5S      �� 
�������$��2q�65   
�������._�5T      �  5U   "   �� 5V   &   $��30 �64   �� 5W   &   .+�5Y   "   ]� -   5        $��TitleBar��5Y   C   
���������.  .+�5Z   &   �� $
��Left=�6,   $��pop弹出窗口��6B   �� 5[   &   -   5        $��BtnClose��5[   �   $��Right=�6-   $��0,2,2,2q�6�   $ 关闭��6�   ��6D   Z� ��6Z   Z� ��6q   Z� ��5\   *   �� 5]   .   $��/Images/close.png��64   6Q   5_   '   5`   #   � 5a   "   $��1��6*    � Points- �    ��@x}@Ys ��@x}@Ys���
����  �6[   � 5b   "   $��1��6*   ) �    ��@x}�hx@x}�hx���
���  ��6W   5d      5e      5f      5g      5h      5j      �� 5k   
   ��5l      �� 5m      $��Expander控件��6   $��17(�6-   $
��Bold"�6;   .��5o      A� $ 2��6   $ 2��6#   .+�5p      9� $��5q�6   $��5q�6&    9� ExpandDirection$
 Down=�62   $��
Region One��6I   .��5q      �� $
��Wrap=�6    $����This eBook is for the use of anyone anywhere at no cost and with almost no restrictions whatsoever.  You may copy it, give it away or re-use it under the terms of the Project Gutenberg License included with this eBook or online at www.gutenberg.org/license��64   5u      9� 5v      $��5q�6   $��5q�6&   $
 Left=�62   $��
Region Two��6I   .��5w      �� $
��Wrap=�6    $����This eBook is for the use of anyone anywhere at no cost and with almost no restrictions whatsoever.  You may copy it, give it away or re-use it under the terms of the Project Gutenberg License included with this eBook or online at www.gutenberg.org/license��64   5{      9� 5|      $��5q�6   $��5q�6&   $ Right=�62   $��Region Three��6J   .��5}      �� $
��Wrap=�6    $����This eBook is for the use of anyone anywhere at no cost and with almost no restrictions whatsoever.  You may copy it, give it away or re-use it under the terms of the Project Gutenberg License included with this eBook or online at www.gutenberg.org/license��64   5�      9� 5�      $��5q�6   $��5q�6&   $ Up=�62   $��Region Four��6G   .��5�      �� $
��Wrap=�6    $����This eBook is for the use of anyone anywhere at no cost and with almost no restrictions whatsoever.  You may copy it, give it away or re-use it under the terms of the Project Gutenberg License included with this eBook or online at www.gutenberg.org/license��64   5�      5�      5�      5�      5�      