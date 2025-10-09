window.blazorHelpers = {
  focusElement: function (element) { },
  initializeDropdown: function (buttonId, menuId, dotnetHelper) {
    const button = document.getElementById(buttonId);
    const menu = document.getElementById(menuId);

    if (!button || !menu) return;

    document.addEventListener('click', function (event) {
      if (!button.contains(event.target) && !menu.contains(event.target)) {
        dotnetHelper.invokeMethodAsync('CloseDropdown');
      }
    });

    document.addEventListener('keydown', function (event) {
      if (event.key === 'Escape') {
        dotnetHelper.invokeMethodAsync('CloseDropdown');
      }
    });
  },
  // TODO: 
  cleanUpDropdown: function() {
    document.removeEventListener('click');
    document.removeEventListener('keydown');
  }

};