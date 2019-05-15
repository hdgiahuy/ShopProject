/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	config.language = 'vi';
	config.uiColor = '#00bfff';
    config.height = 250;
    config.width = 894;
	
	config.toolbarGroups = [
		{ name: 'document', groups: [ 'document', 'doctools', 'mode' ] },
		{ name: 'clipboard', groups: [ 'undo' ] },
		{ name: 'editing', groups: [ 'selection', 'editing' ] },
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph', groups: [ 'list', 'align', 'paragraph' ] },
		'/',
		{ name: 'insert', groups: [ 'insert' ] },
		{ name: 'links', groups: [ 'links' ] },
		{ name: 'styles', groups: [ 'styles' ] },
		{ name: 'colors', groups: [ 'colors' ] },
		{ name: 'tools', groups: [ 'tools' ] },
		{ name: 'others', groups: [ 'others' ] }
	];
	config.removeButtons = 'Save,Print,Cut,Copy,Paste,PasteText,PasteFromWord,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Strike,Subscript,Superscript,Outdent,Indent,CreateDiv,Blockquote,BidiLtr,BidiRtl,Language,Anchor,Flash,SpecialChar,PageBreak,Styles,About,Find,Replace';
	
    config.filebrowserBrowseUrl = 'http://localhost:61765/Scripts/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = 'http://localhost:61765/Scripts/ckfinder/ckfinder.html?type=Images';
    config.filebrowserUploadUrl = 'http://localhost:61765/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = 'http://localhost:61765/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
};