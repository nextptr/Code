   M S B A M L   `   `   `  ����B  >WpfBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfBase.Chapter10WpfBase.Chapter10  5           WpfBase.Chapter10.unit10_1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:WpfBase.Chapter10     5        � 	Resources  5	         	HappyFace j� TileMode j� ViewportUnits j� Viewport �� ImageSource �� Opacity  	TileBrush�� 5   
   %�   &	        &	 q     �� 5      $
 Tile=�6+   $ Absolute=�6;   $ 	0 0 32 32�6T   $ Image/face1.bmp��6i   $	 0.3Q�6�   �� 5      $
 Tile=�6+   $ Absolute=�6;   $ 	0 0 32 32�6T   $ Image/face2.bmp��6i   $	 0.3Q�6�   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6    $��资源引用��60   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� #��[ 6   $��A Tiled Button��6A   $��5q�6Z   $��5q�6e   $
��Bold"�6q   $��14(�6�   �� 5      $��A Normal Button��6   $��5q�63   $��5q�6>   $
��Bold"�6J   $��14(�6\   �� 5      #���  6   $��Another Tiled Button��6B   $��5q�6a   $��5q�6l   $
��Bold"�6x   $��14(�6�   5      5       �� 5!   
   $��0��6   $��1��6    $��动态资源��60   .��5"      �� $ Vertical=�6   .+�5#      �� -   5        $��btn_dynamic1��5#      $��动态资源��6-   $��5q�6<   $��5q�6G   $
��Bold"�6S   $��14(�6e   �� 5$      -   5        $��btn_dynamic2��5$      $��资源查找��6-   $��5q�6<   $��5q�6G   $
��Bold"�6S   $��14(�6e   5%      5&      �� 5'   
   $��1��6   $��0��6    $��系统资源��60   .��5(      �� $ Vertical=�6   .+�5+      �� $��系统颜色��6   #��Z��6'   �� 5,      $��选中高亮画刷��6   #��Z,�6)   5-      5.      5/      50      