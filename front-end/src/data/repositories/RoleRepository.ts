import { Role } from "../entities/Role";
import Repository from "../Repository";

export default class RoleRepository extends Repository<Role> {
  constructor() {
    super("Role");
    console.log("construidad, Rolerepository");
  }
}
