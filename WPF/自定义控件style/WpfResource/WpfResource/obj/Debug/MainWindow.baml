   M S B A M L   `   `   `  ����F  BWpfResource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfResourceWpfResource  5   	   ;"clr-namespace:WpfResource.ResourceWpfResource.Resource  5   	       WpfResource.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WpfResource     5   	   3core"clr-namespace:WpfResource.Resource     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)    � 	Resources 5
       �� 
TargetType   DefaultTextBox �� BasedOn  LabelTextBox0   *WpfResource.Resource.ControlAttachProperty �� Property �� Value �� 
TargetType�� 5   
   %G  '��       7   &	 "     7   �� 5      " ��6   8   6)   �� 5      " ��6   8   .�5      ��  �@ LabelTemplate 5      �� 5      " ��6*   .R�5      �� $��60��6%   
�������._�5   "   �� $��Center=�6,   $��Right=�6G   $��3q�6c   #��z��6n   6�   5      5      5      5      5      .��5      A�  A� Rows$ 8��6    A� Columns$ 5��6   .+�5   
   �� $��默认按钮��6   �� 5   
   $��自定义按钮��6     SignalButton#��[ 6!   �� 5   
   $	��140��6   $��40��6   $��3q�6*   	 �� TextWrapping$
	 Wrap=�65   "
 �� VerticalScrollBarVisibility$
 Visible=�6I   .��6o   3333333333333336�   �� 5   
   $	��150��6   $��30��6   $��3q�6*      	Watermark$ 我是水印��65      CornerRadius$ 2��6a   6�   �� 5   
   $	��150��6   $��30��6   $��3q�6*    �� 
IsReadOnly . $ 15��6G    J� SnapsToDevicePixels . .��6�   我是只读的6�   �� 5    
   $	��150��6   $��30��6   $��3q�6*   {�.  .��6G   IsEnabled="False"6Z   �� 5!   
   $	��150��6   $��30��6   $ 我是水印��6*   6X   �� 5"   
   $	��150��6   $��30��6   $��3q�6*   $ 请输入姓名��65   #��[ 5#         Label$ 	姓名：��6@   6i   5$      5%      