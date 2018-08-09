import { Injectable } from "../../../node_modules/@angular/core";
import { CanDeactivate } from "../../../node_modules/@angular/router";
import { MemberEditComponent } from "../members/member-edit/member-edit.component";

@Injectable()
export class PreventUnsavedChanges
  implements CanDeactivate<MemberEditComponent> {
  canDeactivate(component: MemberEditComponent) {
    if (component.editForm.dirty) {
      return confirm("Jesteś pewny stracisz wszystkie zmiany");
    }
    return true;
  }
}
