   M S B A M L   `   `   `  ���� Q  MXLY.Framework.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:XLY.Framework.Controls;assembly=XLY.Framework.ControlsXLY.Framework.Controls   XLY.Framework.WPFTest,   &XLY.Framework.WPFTest.Page_ImageViewer   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     TxlyDclr-namespace:XLY.Framework.Controls;assembly=XLY.Framework.Controls       N� 
Background  �#5656CA N� Title$ Page_ImageViewer�� � 	Resources    ThumbImageItem � ToolTip �� Path � RowDefinitions+   %XLY.Framework.Controls.ThumbnailImage   AsyncEnable   ThumbnailSource �� TextAlignment  ImageListViewItem	 �� 
TargetType
 �� Property  TextForeground �� Value �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName  ItemSelectedBackground  ItemSelectedForeground  FocusBorderBrush  ItemMouseOverBackground  ItemMouseOverForeground  MouseOverBorderBrush ]� 
Conditions �� Property �� Value��%  &	        &	 .    7  7  7  7  7  7  7  7  7  7  �� .R�� $	��140��$	��120�� �� $ DataContext.FullPath� �� $��* ��� $��20 �.+�  $	��140��$	��100�� . $��Center=�$��Center=�$
��None=� �� *File+�� $��1��$��12(�$��20��$��Center=�$��Center=�$ Center=�$��CharacterEllipsis=����� *Name+�� "	 ��.��� 
 � ��8   �� 
 � ��$ Stretch=��� 
 � ��$ Center=��� 
 � ��$ 2q��� 
 �@J�SnapsToDevicePixels . �� 
 � ��
 ���� �� 
 � ��$ 2,0,2,0q��� 
 � �� �� " ��.R��� $ Bd��#��z�� . $��1q�
������ #��z��._��� $ contentPresenter��#��z��#��z�� P�  �@��
IsSelected . . ��� $ Bd��8  
 � ���� 8  
 � ���� $ Bd��8  
 � ��P�  �@J�IsMouseOver . . ��� $ Bd��8  
 � ���� 8  
 � ���� $ Bd��8  
 � ��]�  ��  �@��
IsSelected . ��  �@��IsSelectionActive . ./��� 8  
 � ���� 8  
 � ���� $ Bd��8 	 
 � ��.��� $��3q� �� $��50 ��� $��* �.+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ 	txtFolder�� 	 LabelOpenFolderTextBox#��[	 $��30��$	��400��$��5q�.��D:\Doc\Resource$   XLY.Framework.Controls.FButton  -   $��绑定��$��5q��� -    ��Name$ 
timgViewer��$��1�� �� AlternationCount$ 0��! �� IsDeferredScrollingEnabled .  �� SelectionMode$ Multiple=�#��[  #��[ ���� .R�2   ,XLY.Framework.Controls.VirtualizingWrapPanel     
ItemHeight$	 200��   	ItemWidth$	 240��   Orientation$ 
Horizontal=�  $� IsVirtualizing  . ! $� VirtualizationMode$! 	Recycling=�"   CanVerticallyScroll" . #   CanHorizontallyScroll# .  