   M S B A M L   `   `   `  ����A  =WpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WpfAppWpfApp  5          WpfApp.panel1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:WpfApp     5      .��5	      A�   A� Rows$  2��6    A� Columns$ 3��6   .+�5
   
   �� $��直线��6   .��5      � .+�5      �� 	 �� X1$ 10��6   	 �� Y1$ 20��6   	 �� X2$	 200��6(   	 �� Y2$ 20��61   
���  ��$��7��6I   �� 5      $ 10��6   $ 40��6   $	 200��6(   $ 40��61   
���   �$��6��6I    �� StrokeDashArray$ 5R�6]   �� 5      $ 10��6   $ 60��6   $	 200��6(   $ 60��61   
���   �$��6��6I    �� StrokeEndLineCap$
 Flat=�6]   �� 5      $ 10��6   $ 80��6   $	 200��6(   $ 80��61   
���   �$��6��6I   $ Triangle=�6]   �� 5      $ 10��6   $	 100��6   $	 200��6(   $	 100��61   $��6��6:   $ Round=�6N   ��5      �� 5       �� EndPoint$ 0,0.5&�6.   	 �� 
StartPoint$	 1,0.5&�6?   .��5      � 
 � Color$

 Blue��6+   � 5       � Offset$ 1Q�6+   5      5      5      5      �� 5   
   $��矩形��6   .��5      A� $  2��6   $ 3��6#   .+�5      �� 
���   �
����ح�6+   �� 5      ��5      �� 5      $		 0,0&�6.   $	 1,1&�6?   .��5       � $
 	#FFB6F8F1��6+   $ 0Q�6=   � 5!      $
 	#FF0082BD��6+   $
 0.25Q�6=   � 5"      $
 	#FF95DEFF��6+   $	 0.6Q�6=   � 5#      $
 	#FF004F72��6+   $ 1Q�6=   5$      5&      �� 5'      ��5(      
� 5)      .��5*      � $
 	#FFB6F8F1��6+   $ 0Q�6=   � 5+      $
 	#FF0082BD��6+   $
 0.25Q�6=   � 5,      $
 	#FF95DEFF��6+   $
 0.75Q�6=   � 5-      $
 	#FF004F72��6+   $	 1.5Q�6=   5.      50      �� 51      ��52      �� 53       �� ImageSource$ img.jpg��6%    j� Viewport$ 0,0,0.3,0.15�6;    j� TileMode$
 Tile=�6S   55      �� 56      ��57      L� 58      $ 0,0,0.2,0.2�6'   $
 Tile=�6>    L� Drawing 59      � 5:   "    � Brush
 ��ح� � Geometry 5;   &   @� 5<   *    @� RadiusX$ 10Q�6:    @� RadiusY$ 10Q�6G   5>   #   5@      5B      �� 5C      $��10��6   ��5D      �� 5E      $		 0,0&�6.   $	 1,1&�6?   .��5F      � $
 White��6+   $	 0.3Q�69   � 5G      $

 Blue��6+   $ 1Q�69   5H      5J      5K      5L      �� 5M   
   $��椭圆��6   .��5N      A� 
�������$	��140��6$   $	��140��60    � Cursor$
 Hand��6=    � ToolTip$ A Ball��6K   ��5O      
� 5P       
� GradientOrigin$ 0.2,0.8&�6*    
� RadiusX$
 0.75Q�6C    
� RadiusY$
 0.75Q�6R    �� RelativeTransform 5Q      V� 5R      .~�5S   "   ��  �� Angle$ 90Q�62    �� CenterX$	 0.5Q�6=    �� CenterY$	 0.5Q�6K   T� 5T   "   5U      .��5W      � $
 	#FFFFFFFF��6'   $ 0Q�69   � 5X      $
 	#FF444444��6'   $
 0.66Q�69   � 5Y      $
 	#FF999999��6'   $ 1Q�69   5Z      5\      5]      �� 5^   
   $��
基本path��6   .��5_      A� $  2��6   $ 2��6#   .+�5`      F� 
����  �$��2��6%   ��5a      �� 5b       �� 
StartPoint$ 20,20&�6'    �� EndPoint$ 140,100&�6:   5d      F� 5e      
��� ���
��� �����5f      �� 5g       �� Rect$ 20,20,120,120�6,     �� RadiusX$  10Q�6A   ! �� RadiusY$! 10Q�6N   5i      F� 5j      
��� � �
��� �|���5k      @� 5l      " @� Center$" 80,80&�6*   $ 30Q�69   $ 30Q�6F   5n      F� 5o      
��� ���
��� �����5p      B� 5q      ��5s   "   E� # E� 
StartPoint$# 20,140&�6-   $ E� IsClosed$ . ��5u   *   �� % �� Point$% 20,40&�66   �� 5v   *   $% 40,110&�66   �� 5w   *   $% 50,20&�66   �� 5x   *   $% 80,110&�66   �� 5y   *   $% 110,20&�66   �� 5z   *   $% 120,110&�66   �� 5{   *   $% 140,40&�66   �� 5|   *   $% 135,140&�66   5~   #   5�      5�      5�      5�      �� 5�   
   $��贝塞尔曲线��6   .��5�      A� $  2��6   $ 2��6#   .+�5�      F� 
���   �$��2��6&   ��5�      B� 5�      .��5�      E� $	# 0,0&�6)   .��5�   "   �� & �� Point1$& 250,0&�60   ' �� Point2$' 20,250&�6?   ( �� Point3$( 300,200&�6O   5�      5�      5�      F� 5�      
���   �$��2��6&   ��5�      B� 5�      .��5�      E� $# 0,200&�6)   .��5�   "   � ) � Point1$) 150,-100&�69   * � Point2$* 150,200&�6K   5�      5�      5�      F� 5�      
���   �
����ح�$��1��67   ��5�      � 5�      .��5�      B� .��5�   "   E� $	# 0,0&�6-   .��5�   &   �� $& 250,0&�64   $' 50,200&�6C   $( 300,200&�6S   5�   #   5�      B� 5�      .��5�   "   E� $	# 0,0&�6-   .��5�   &   �� $& 230,0&�64   $' 50,200&�6C   $( 300,200&�6S   5�   #   5�      B� 5�      .��5�   "   E� $	# 0,0&�6-   .��5�   &   �� $& 210,0&�64   $' 50,200&�6C   $( 300,200&�6S   5�   #   5�      B� 5�      .��5�   "   E� $	# 0,0&�6-   .��5�   &   �� $& 190,0&�64   $' 50,200&�6C   $( 300,200&�6S   5�   #   5�      B� 5�      .��5�   "   E� $	# 0,0&�6-   .��5�   &   �� $& 170,0&�64   $' 50,200&�6C   $( 300,200&�6S   5�   #   5�      B� 5�      .��5�   "   E� $	# 0,0&�6-   .��5�   &   �� $& 150,0&�64   $' 50,200&�6C   $( 300,200&�6S   5�   #   5�      B� 5�      .��5�   "   E� $	# 0,0&�6-   .��5�   &   �� $& 130,0&�64   $' 50,200&�6C   $( 300,200&�6S   5�   #   5�      5�      5�      5�      5�      �� 5�   
   $��标记语法��6   .��5�      A� $ 2��6   .+�5�      F� 
���  ��B����        Ӏ��    �, �� �� ��Ӏ�� ����!
 ��	6&   F� 5�      
���   �B����        Ӏ��    �, �� �� ��Ӏ�� ����!
 ��	6&   5�      5�      5�      5�      