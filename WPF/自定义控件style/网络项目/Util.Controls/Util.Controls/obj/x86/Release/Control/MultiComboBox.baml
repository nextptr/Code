   M S B A M L   `   `   `  ���� Q  MXLY.Framework.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:XLY.Framework.ControlsXLY.Framework.Controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   CheckItemStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels �� Orientation	 d� 	IsChecked
 �� Path �� RelativeSource �� Mode �� Triggers P� Property P� Value �� 
TargetName  ItemMouseOverBackground  ItemMouseOverForeground  DisableOpacity  DefaultMultiComboBox*    $XLY.Framework.Controls.MultiComboBox  TextForeground  TextBackground  ControlBorderBrush2   ,XLY.Framework.Controls.ControlAttachProperty  FocusBackground 	 FocusBorderBrush 
 MouseOverBorderBrush  
FontFamily  FontSize �Name � ColumnDefinitions ��Name   LabelTemplate J� IsHitTestVisible   Label K� ZIndex   AttachContent d�Name  ComboToggleButton   FocusBackground'   !XLY.Framework.Controls.XConverter   TrueToFalseConverter �� 	Converter ��Name �� Padding   	Watermark  J� Opacity! �� TextAlignment" ��Name  EditableTextBoxStyle# �� 
IsReadOnly$ �� 
IsReadOnly% �Name& � AllowsTransparency' � 	StaysOpen( ��Name) �� SelectionMode* �� MaxDropDownHeight+ �� Value, �� Binding  ClearButtonMultiComboBox- �� BasedOn$   XLY.Framework.Controls.FButton.   FIcon  FButton_Transparency/   FIconMargin'0    IsClearTextButtonBehaviorEnabled1   	FIconSize2 � Cursor3 �� AncestorType  LabelMultiComboBox  TextLabelBackground ! LabelClearButtonMultiComboBox�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    .local$clr-namespace:XLY.Framework.Controls  :x,http://schemas.microsoft.com/winfx/2006/xaml       �� $U Opack://application:,,,/XLY.Framework.Controls;component/Style/Themes/Share.xaml?��� $\ Vpack://application:,,,/XLY.Framework.Controls;component/Style/Themes/ToggleButton.xaml?�%�  &	        7  7  7  &	 w    7  7  7  7  7 	 7 
 7  7  7  7  7   7  &	 �    7  7  7  &	 �    7  7  &	 k    7  7  7  7  '   $    7  �� " ��.���  � �� �� " ��.R��� $ Border��#��z��# z ._��� $ 
Horizontal=�$��Center=�.+��� 	 �� $
 
IsSelected� �� *TemplatedParent+$ TwoWay=�.���� #��z��# z #��z��#��z�� P�  �@J�IsMouseOver . . ��� $ Border��8    � ���� 8   � ��P�  � {� .  . ��� $ Border��8   �@J�Opacity�� "   .���  � ��$ 30����  � ��$	 200����  � ��8   ��  � ��8  ��  � ��$ Stretch=���  � ��$ Center=���  � ��$ 1q���  � ��8  ��  �@ FocusBackground8  ��  �@ FocusBorderBrush8  ��  �@ MouseOverBorderBrush8  ��  � ��8  ��  � ��8  ��  �@J�SnapsToDevicePixels . ��  �@��MaxDropDownHeight$	 400����  � �� .  ��  � �� �� "   .R�� $ 	PART_Root��.+��� $ Bg��# z #��z��#��z��#��z��� $ PART_InnerGrid��$��0q� �� $
��Auto ��� $��* ��� $
��Auto ��� $��21 �.+��� $ Label��#��z ��.   .  #��z $��1,1,0,1q��� $ PART_AttachContent��$ 2��$��2��$��Center=�$��Center=�._��� $��Center=�$��Center=�#��z d� $ PART_DropDownToggle��$ 1����.  8�� $��1��$��3��$��2 1 2 1q�#��z 	 �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+{��� $
 
IsReadOnly� �� *TemplatedParent+# Z $ OneWay=��� $��1��._��� $ Message��$ 0q�$y�	Collapsed=�#��z #��z�� .  $	  0.6Q�$
��Left=�$! Center=�$��Center=�$��5,2,5,2q�� $��1��$��2 0 0 0q�.+��� $" PART_EditableTextBox��8��	 #��z��$��Stretch=�# z  . #��z��#��z��#$ z# #��z��#��z������ $

 Text�$ TwoWay=� �� *TemplatedParent+� $% 
PART_Popup��& . |�.  ' .  #��� .�$��Bottom=����� *IsDropDownOpen+ �� *TemplatedParent+.��� ���� *ActualWidth+ �� *TemplatedParent+���� *MaxDropDownHeight+ �� *TemplatedParent+.+��� $ PopupBorder��#��z��$��Stretch=�$
��Auto��#��z��#��z���� $( PART_ListBox��$) Multiple=�#��z��#��z* 8��
 ���� *ItemsSource+ �� *TemplatedParent+ �� $+  ��, ��  �� *Self+$

 Text�.a��� $ Message�� � y�$ Visible=�P�  �@��
IsEditable . . ��� $ PART_DropDownToggle�� � ��$ 3���� $ PART_DropDownToggle�� � ��$ 1���� $ PART_DropDownToggle�� � ��
 ���� ��  � �� .  �� $ PART_DropDownToggle�� � |� .  P�  �@J�IsMouseOver . . ���  �� $8
 2(local:ControlAttachProperty.MouseOverBorderBrush)� �� *Self+ � ��P�  �@J�	IsFocused . . ���  �� $4
 .(local:ControlAttachProperty.FocusBorderBrush)� �� *Self+ � ��P�  �@J�IsKeyboardFocusWithin . . ���  �� $4
 .(local:ControlAttachProperty.FocusBorderBrush)� �� *Self+ � ��P�  � {� .  . ��� $ 	PART_Root��8   �@J�Opacity�� "   8-   .���  �@ AttachContent �� .R�  $	. ��8�� ��.  $/ 0q�0 . $2��,local:ControlAttachProperty.ClearTextCommand��$��1,3,0,4q�$1 13��8�� $2 Arrow������  �� *FindAncestor+"3   {��� $
 
IsReadOnly� �� *FindAncestor+"3   # Z $ OneWay=��� "   8-   .���  � ��$	 260����  �@ LabelTemplate �� " ��.R��� $��60��8�� ._��� $��Center=�$��Right=�$��2q�#��z���� "   8-   .���  � ��$	 280����  �@ LabelTemplate �� " ��.R��� $��60��8�� ._��� $��Center=�$��Right=�$��2q�#��z����  �@ AttachContent �� .R�  $	. ��8�� ��.  $/ 0q�0 . $2��,local:ControlAttachProperty.ClearTextCommand��$��0,3,0,4q�$1 13��8�� $2 Arrow������  �� *FindAncestor+"3   {��� $
 
IsReadOnly� �� *FindAncestor+"3   # Z $ OneWay=��� "   8-   